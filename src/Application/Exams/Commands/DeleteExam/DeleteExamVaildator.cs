using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Exams.Commands.DeleteCourse;

namespace WeCare.Application.Exams.Commands.DeleteExam;
public class DeleteExamVaildator : AbstractValidator <DeleteExamCommand>
{
    private readonly IApplicationDbContext _context;

    public DeleteExamVaildator(IApplicationDbContext applicationDbContext) 
    {

        _context = applicationDbContext;
        RuleFor(v => v.Date).Must(IsExamExists)
            .NotEmpty().NotNull();
    }
    private bool IsExamExists(DateTime data)
    {
        return _context.Exams.AsNoTracking().Any(c => c.DueDate == data);
    }


}


