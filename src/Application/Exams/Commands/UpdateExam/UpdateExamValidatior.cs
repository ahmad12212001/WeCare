using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace WeCare.Application.Exams.Commands.UpdateExam;
public class  UpdateExamValidatior : AbstractValidator<UpdateExamCommand>

{
    private readonly IApplicationDbContext _context;

    public UpdateExamValidatior(IApplicationDbContext applicationDbContext) {
        _context = applicationDbContext;
        RuleFor(v => v.OldDueDate).Must(IsExamExists).NotEmpty().NotNull();
        RuleFor(v => v.Location).NotEmpty().NotNull();
        RuleFor(v => v.Hallno).NotEmpty().NotNull();
        RuleFor(v => v).Must(v => IsExamNewNameNotExist(v.OldDueDate))
          .NotEmpty().NotNull();


    }
    private bool IsExamExists(DateTime date)
    {
        return _context.Exams.AsNoTracking().Any(c => c.DueDate == date);

    }

    private bool IsExamNewNameNotExist(DateTime date)
    {
        return !(_context.Exams.AsNoTracking().Any(c => c.DueDate == date));
    }

}
