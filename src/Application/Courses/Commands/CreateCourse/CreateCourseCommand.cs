using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;
using WeCare.Domain.Entities;

namespace WeCare.Application.Courses.Commands.CreateCourse;

[Authorize(Roles = "AcademicStaff")]
public record CreateCourseCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
}

public class CreateCourseCommandHandler : IRequestHandler<CreateCourseCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;
    public CreateCourseCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _context = applicationDbContext;
        _currentUser = currentUserService;
    }

    public async Task<int> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
    {
        var course = new Course
        {
            Name = request.Name,
            UserId = _currentUser?.UserId!
        };

        await _context.Courses.AddAsync(course);

        await _context.SaveChangesAsync(cancellationToken);

        return course.Id;
    }
}