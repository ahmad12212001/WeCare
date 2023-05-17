using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;

namespace WeCare.Application.Courses.Commands.UpdateCourse;
public class UpdateCourseValidatior : AbstractValidator<UpdateCourseCommand>
{
    private readonly IApplicationDbContext _context;

    public UpdateCourseValidatior(IApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
        RuleFor(v => v.Id).Must(IsCourseExists)
            .NotEmpty().NotNull();
        RuleFor(v => v.Name).NotEmpty().NotNull();
        RuleFor(v => v).Must(v => IsCourseNewNameNotExist(v.Id, v.Name))
          .NotEmpty().NotNull();
    }

    private bool IsCourseExists(int courseId)
    {
        return _context.Courses.AsNoTracking().Any(c => c.Id == courseId);
    }

    private bool IsCourseNewNameNotExist(int courseId, string courseName)
    {
        return !(_context.Courses.AsNoTracking().Any(c => c.Name == courseName && c.Id != courseId));
    }
}