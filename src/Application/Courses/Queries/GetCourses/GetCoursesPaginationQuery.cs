using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Mappings;
using WeCare.Application.Common.Models;
using WeCare.Application.Common.Security;
using WeCare.Application.Courses.Dtos;

namespace WeCare.Application.Courses.Queries.GetCourses;
[Authorize]
public record GetCoursesPaginationQuery : IRequest<PaginatedList<CourseDto>>
{
    public string? Name { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetCoursesPaginationQueryHandler : IRequestHandler<GetCoursesPaginationQuery, PaginatedList<CourseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCoursesPaginationQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CourseDto>> Handle(GetCoursesPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _context.Courses
            .Where(x => !string.IsNullOrEmpty(request.Name) ? x.Name.Contains(request.Name) : true)
            .OrderBy(x => x.Name)
            .Select(i => new CourseDto
            {
                Id = i.Id,
                MajorGroupId = i.MajorGroupId,
                AccadmeicStaffName = $"{i.User.FirstName} {i.User.LastName}",
                MajorGroupName = i.MajorGroup.Name,
                Name = i.Name,
                UserId = i.UserId
            })
            .PaginatedListAsync(request.PageNumber, request.PageSize);

    }

}
