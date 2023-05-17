using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Models;
using WeCare.Domain.Entities;
using WeCare.Domain.Enums;

namespace WeCare.Application.Students.Commands.CreateStudent;
public record CreateStudentCommand : IRequest<int>
{
    public string Id { get; set; } = null!;
    public string Major { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public StudentType Type { get; set; }
}

public class CreateStudentCommandHandler : IRequestHandler<CreateStudentCommand, int>
{

    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IEmailService _emailService;
    private readonly IPasswordGenerator _passwordGenerator;
    public CreateStudentCommandHandler(
        IApplicationDbContext applicationDbContext,
        ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager,
        IEmailService emailService, IPasswordGenerator passwordGenerator)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
        _userManager = userManager;
        _passwordGenerator = passwordGenerator;
        _emailService = emailService;
    }
    public async Task<int> Handle(CreateStudentCommand request, CancellationToken cancellationToken)
    {

        var applicationUser = new ApplicationUser()
        {
            Email = request.Email,
            FirstName = request.FirstName,
            LastName = request.LastName,
            IsActive = true,
            NormalizedEmail = request.Email.ToUpper(),
            UserName = request.Email,
            NormalizedUserName = request.Email.ToUpper(),
            PhoneNumber = request.PhoneNumber
        };

        var password = _passwordGenerator.GeneratePassword();

        var result = await _userManager.CreateAsync(applicationUser, password);

        if (result.Succeeded)
        {

            //to be changed after implementation
            await _emailService.SendEmailAsync(new EmailMessage
            {
                Subject = "Registration Completed",
                Content = "You have successfully Registered in (We care). \n" +
                "Kindly Find you Login options. \n" +
                "UserName:" + request.Email + "\n" +
                "Password:" + password,
                To = new Dictionary<string, string>()
                {
                    {request.FirstName, request.Email }
                }
            });

            await _userManager.AddToRoleAsync(applicationUser, request.Type.ToString());

            var major = await _applicationDbContext.Majors.FirstAsync(m => m.Name == request.Major);

            Student? student = null;

            switch (request.Type)
            {
                case StudentType.DisabilityStudent:
                    student = new DisabilityStudent
                    {

                        StudentId = request.Id,
                        MajorId = major.Id,
                        UserId = applicationUser.Id

                    };
                    break;
                case StudentType.VolunteerStudent:
                    student = new VolunteerStudent
                    {

                        StudentId = request.Id,
                        MajorId = major.Id,
                        UserId = applicationUser.Id

                    };
                    break;
                default:
                    break;
            }

            if (student != null)
            {
                await _applicationDbContext.Students.AddAsync(student);
                await _applicationDbContext.SaveChangesAsync(cancellationToken);
                return student.Id;
            }


        }

        return 0;

    }

}
