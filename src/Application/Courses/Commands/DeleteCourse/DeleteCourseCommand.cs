using System.Data;
using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;

namespace WeCare.Application.Courses.Commands.DeleteCourse;

[Authorize(Roles = "AcademicStaff")]
public record DeleteCourseCommand : IRequest<string>
{
    public int CourseId { get; set; }
}

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, string>
{
    private readonly IApplicationDbContext _context;
    public DeleteCourseCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }
    public async Task<string> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = (await _context.Courses.FindAsync(request.CourseId))!;
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync(cancellationToken);
        return course.Name;
    }
}
