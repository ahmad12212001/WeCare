using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Courses.Dtos;
using WeCare.Domain.Entities;

namespace WeCare.Application.StudentCourses.Query.GetStudentAvailableCourses;
public record GetStudentAvailableCoursesQuery : IRequest<List<CourseDto>>
{
    public int StudentId { get; set; }
}

public class GetStudentAvailableCoursesQueryHandler : IRequestHandler<GetStudentAvailableCoursesQuery, List<CourseDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    public GetStudentAvailableCoursesQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<List<CourseDto>> Handle(GetStudentAvailableCoursesQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.StudentCourses.Where(i => i.StudentId == request.StudentId)
                     .Select(i => i.Course).Select(i => new CourseDto
                     {
                         Id = i.Id,
                         MajorGroupId = i.MajorGroupId,
                         AccadmeicStaffName = $"{i.User.FirstName} {i.User.LastName}",
                         MajorGroupName = i.MajorGroup.Name,
                         Name = i.Name,
                         UserId = i.UserId
                     })
                     .ToListAsync();
    }
}
