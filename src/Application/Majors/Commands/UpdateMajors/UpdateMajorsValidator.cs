using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;

namespace WeCare.Application.Majors.Commands.UpdateMajors;
public class UpdateMajorsValidator :AbstractValidator<UpdateMajorsCommand>
{
    private readonly IApplicationDbContext _context;


    public UpdateMajorsValidator(IApplicationDbContext context)
    {
        _context = context;
        RuleFor(v => v.Id).Must(IsMajorExists)
            .NotEmpty().NotNull();
        RuleFor(v => v.Name).NotEmpty().NotNull();
        RuleFor(v => v).Must(v => IsMajorNewNameNotExist(v.Id, v.Name)).NotEmpty().NotNull();
    }

    private bool IsMajorExists(int /**/majorId)
    {
        return _context.Majors.AsNoTracking().Any(m => m.Id == majorId);
    }

    private bool IsMajorNewNameNotExist(int majorId, string majorName) {
        return !(_context.Majors.AsNoTracking().Any(m => m.Name ==majorName && m.Id == majorId));
    }

}
