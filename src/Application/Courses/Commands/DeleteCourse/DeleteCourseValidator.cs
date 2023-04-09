using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;

namespace WeCare.Application.Courses.Commands.DeleteCourse;
public class DeleteCourseValidator : AbstractValidator<DeleteCourseCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteCourseValidator(IApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
        RuleFor(v => v.CourseId).Must(IsCourseExists)
           .NotEmpty().NotNull();
    }

    private bool IsCourseExists(int courseId)
    {
        return _context.Courses.AsNoTracking().Any(c => c.Id == courseId);
    }
}
