using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;

namespace WeCare.Application.Majors.Commands.CreateMajor;
public class CreateMajorCommandValidation : AbstractValidator<CreateMajorCommand>
{
    private readonly IApplicationDbContext _context;

    public CreateMajorCommandValidation(IApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext; 
        RuleFor(v => v.Name).Must(IsMajorNotExists)
            .MaximumLength(200)
            .NotEmpty().NotNull();
    }

    private bool IsMajorNotExists(string courseName)
    {
        return !(_context.Courses.AsNoTracking().Any(c => c.Name == courseName));
    }
}