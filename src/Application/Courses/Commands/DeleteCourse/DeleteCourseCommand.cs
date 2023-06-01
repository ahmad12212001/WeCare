using System.Data;
using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;

namespace WeCare.Application.Courses.Commands.DeleteCourse;

[Authorize(Roles = "DeanOffice")]
public record DeleteCourseCommand : IRequest<int>
{
    public int CourseId { get; set; }
}

public class DeleteCourseCommandHandler : IRequestHandler<DeleteCourseCommand, int>
{
    private readonly IApplicationDbContext _context;
    public DeleteCourseCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }
    public async Task<int> Handle(DeleteCourseCommand request, CancellationToken cancellationToken)
    {
        var course = (await _context.Courses.FindAsync(request.CourseId))!;
        _context.Courses.Remove(course);
        await _context.SaveChangesAsync(cancellationToken);
        return course.Id;
    }
}
