using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;
using WeCare.Application.Courses.Dtos;

namespace WeCare.Application.Courses.Queries.GetCourses;
[Authorize]
public record GetAcademicStaffCoursesQuery : IRequest<List<CourseDto>>
{
}
public class GetAcademicStaffCoursesQueryHandler : IRequestHandler<GetAcademicStaffCoursesQuery, List<CourseDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;
    private readonly ICurrentUserService _currentUserService;

    public GetAcademicStaffCoursesQueryHandler(IApplicationDbContext context, IMapper mapper,ICurrentUserService currentUserService)
    {
        _context = context;
        _mapper = mapper;
        _currentUserService = currentUserService;
    }

    public async Task<List<CourseDto>> Handle(GetAcademicStaffCoursesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Courses.Where(c => c.UserId == _currentUserService.UserId!)
            .OrderBy(x => x.Name)
            .ProjectTo<CourseDto>(_mapper.ConfigurationProvider).ToListAsync();
        

    }

}
