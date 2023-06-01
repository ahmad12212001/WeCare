using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;
using WeCare.Domain.Enums;

namespace WeCare.Application.Students.Commands.UpdateStudent;
public record UpdateStudentCommand : IRequest<Student?>
{
    public string StudentId { get; set; } = null!;
    public string Major { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public int Id { get; set; }
    public StudentType Type { get; set; }
    public string? UserId { get; set; }
}


public class UpdateStudentCommandHandler : IRequestHandler<UpdateStudentCommand, Student?>
{
    private readonly IApplicationDbContext _applicationDbContext;
    public UpdateStudentCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<Student?> Handle(UpdateStudentCommand request, CancellationToken cancellationToken)
    {
        var student = await _applicationDbContext.Students.FindAsync(request.Id);

        if (student == null)
        {
            if (!string.IsNullOrEmpty(request.UserId))
            {
                student = await _applicationDbContext.Students.FirstOrDefaultAsync(i => i.UserId == request.UserId);
            }

            if (student == null)
            {
                return null;
            }

        }

        var major = await _applicationDbContext.Majors.FirstOrDefaultAsync(i => i.Name == request.Major);

        if (major == null)
        {
            return null;
        }

        student.User.FirstName = request.FirstName;
        student.User.LastName = request.LastName;
        student.User.Email = request.Email;
        student.User.PhoneNumber = request.PhoneNumber;
        student.StudentId = request.StudentId;
        student.MajorId = major.Id;
        student.Discriminator = request.Type.ToString();
        _applicationDbContext.Students.Update(student);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return student;
    }
}