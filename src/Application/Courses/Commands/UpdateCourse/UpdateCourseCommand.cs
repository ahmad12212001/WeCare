using AutoMapper;
using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;
using WeCare.Application.Courses.Dtos;
using WeCare.Domain.Entities;

namespace WeCare.Application.Courses.Commands.UpdateCourse;

[Authorize(Roles = "AcademicStaff")]
public record UpdateCourseCommand : IRequest<CourseDto>
{
    public int CourseId { get; set; }
    public string Name { get; set; } = null!;
}

public class UpdateCourseCommandHandler : IRequestHandler<UpdateCourseCommand, CourseDto>
{
    private readonly IMapper _mapper;
    private readonly IApplicationDbContext _context;
    public UpdateCourseCommandHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _context = applicationDbContext;
        _mapper = mapper;
    }
    public async Task<CourseDto> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
    {

        var course = (await _context.Courses.FindAsync(request.CourseId))!;
        course.Name = request.Name;
        _context.Courses.Update(course);
        await _context.SaveChangesAsync(cancellationToken);
        return _mapper.Map<CourseDto>(course);
    }
}