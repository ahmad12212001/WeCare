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
public record GetCoursesQuery : IRequest<PaginatedList<CourseDto>>
{
    public string? Name { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}
public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, PaginatedList<CourseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCoursesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Courses
            .Where(x => !string.IsNullOrEmpty(request.Name) ? x.Name.Contains(request.Name) : true)
            .OrderBy(x => x.Name)
            .ProjectTo<CourseDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

    }

}
