using MediatR;
using Microsoft.AspNetCore.Http;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Models;
using WeCare.Domain.Entities;
using WeCare.Domain.Enums;

namespace WeCare.Application.Materials.Commands.CreateMaterial;
public record CreateMaterialCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int CourseId { get; set; }
    public int? RequestId { get; set; }
    public IFormFile File { get; set; } = null!;
}


public class CreateMaterialCommandHandler : IRequestHandler<CreateMaterialCommand, int>
{
    private readonly IBlobStorageService _blobStorageService;
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    private readonly IEmailService _emailService;
    public CreateMaterialCommandHandler(
        IBlobStorageService blobStorageService,
        IApplicationDbContext context, ICurrentUserService currentUserService,
        IIdentityService identityService, IEmailService emailService)
    {
        _blobStorageService = blobStorageService;
        _context = context;
        _currentUserService = currentUserService;
        _identityService = identityService;
        _emailService = emailService;
    }
    public async Task<int> Handle(CreateMaterialCommand request, CancellationToken cancellationToken)
    {
        var course = _context.Courses.FirstOrDefault(c => c.Id == request.CourseId);

        if (course == null)
        {
            return 0;
        }

        var blobRequest = await _blobStorageService.UploadFileAsync(new BlobStorageRequest
        {
            Path = course.Name,
            File = request.File
        });

        if (blobRequest.Result.Succeeded)
        {
            var role = await _identityService.GetUserRoleAsync(_currentUserService.UserId!);

            Material material = new Material
            {
                ContentType = request.File.ContentType,
                CourseId = request.CourseId,
                Description = request.Description,
                Name = request.Name,
                Path = blobRequest.Path
            };

            switch (role)
            {
                case "AcademicStaff":
                    material.MaterialStatus = MaterialStatus.Approved;
                    break;
                case "VolunteerStudent":
                    var student = _context.VolunteerStudents.FirstOrDefault(c => c.User.Id == _currentUserService.UserId!);
                    material.MaterialStatus = MaterialStatus.Pending;
                    material.VolunteerStudentId = student!.Id;
                    material.RequestId = request.RequestId;
                    await SendEmailToAcademicStaffAsync(course.User.FirstName, course!.User!.Email!, student!.User.FirstName, course.Name);
                    break;
            }

            await _context.Materials.AddAsync(material);

            await _context.SaveChangesAsync(cancellationToken);

            return material.Id;
        }

        return 0;
    }

    private async Task SendEmailToAcademicStaffAsync(string name, string email, string studentName, string courseName)
    {
        await _emailService.SendEmailAsync(new EmailMessage
        {
            Content = $"New Material has been Uploaded by: {studentName} for course {courseName} needs review",
            Subject = "Material Approvel",
            To = new Dictionary<string, string>()
                {
                    {email ,name}
                }
        });
    }
}