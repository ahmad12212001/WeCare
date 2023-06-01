using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;

namespace WeCare.Application.Students.Queries;
public record GetStudentQuery : IRequest<StudentDto>
{
    public int StudentId { get; set; }

}
public class GetStudentQueryHandler : IRequestHandler<GetStudentQuery, StudentDto>
{
    private readonly IApplicationDbContext _context;
    public GetStudentQueryHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<StudentDto> Handle(GetStudentQuery request, CancellationToken cancellationToken)
    {
        var student = _context.Students.Select(s =>
        new StudentDto
        {
            Email = s.User.Email!,
            FirstName = s.User.FirstName,
            LastName = s.User.LastName,
            Id = s.Id,
            Major = s.Major.Name,
            PhoneNumber = s.User.PhoneNumber!,
            StudentId = s.StudentId,
            Type = s.Discriminator
        }).AsNoTracking().Single(t => t.Id == request.StudentId);
        return student;



    }
}