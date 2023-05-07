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
        RuleFor(v => v.Location).NotEmpty().NotNull();
        RuleFor(v => v.Hallno).NotEmpty().NotNull();


    }
    private bool IsExamExists(string name)
    {
        return _context.Exams.AsNoTracking().Any(c => c.Name== name);

    }

    private bool IsExamNewNameNotExist(string name)
    {
        return !(_context.Exams.AsNoTracking().Any(c => c.Name == name));
    }

}
