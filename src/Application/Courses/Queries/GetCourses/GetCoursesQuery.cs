using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Courses.Dtos;

namespace WeCare.Application.Courses.Queries.GetCourses;
public record GetCoursesQuery : IRequest<List<CourseDto>>
{
}


public class GetCoursesQueryHandler : IRequestHandler<GetCoursesQuery, List<CourseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCoursesQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<CourseDto>> Handle(GetCoursesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Courses.ProjectTo<CourseDto>(_mapper.ConfigurationProvider).ToListAsync();
    }

}
