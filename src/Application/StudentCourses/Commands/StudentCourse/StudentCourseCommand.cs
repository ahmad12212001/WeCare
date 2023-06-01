using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;

namespace WeCare.Application.StudentCourses.Commands.CreateStudentCourse;
public record StudentCourseCommand : IRequest<Unit>
{
    public int StudentId { get; set; }
    public int[] Courses { get; set; } = null!;
}

public class StudentCourseCommandHandler : IRequestHandler<StudentCourseCommand,Unit>
{
    private readonly IApplicationDbContext _applicationDbContext;
    public StudentCourseCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Unit> Handle(StudentCourseCommand request, CancellationToken cancellationToken)
    {
        var oldStudentCourses = await _applicationDbContext.StudentCourses.Where(i => i.StudentId == request.StudentId).ToListAsync();

        _applicationDbContext.StudentCourses.RemoveRange(oldStudentCourses);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        var studentCourses = request.Courses.Select(i => new StudentCourse
        {
            StudentId = request.StudentId,
            CourseId = i
        });

        await _applicationDbContext.StudentCourses.AddRangeAsync(studentCourses);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

