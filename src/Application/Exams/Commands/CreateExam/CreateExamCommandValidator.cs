using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.VisualBasic;

namespace WeCare.Application.Exams.Commands.CreateExam;
public class CreateExamValiditior : AbstractValidator<CreateExamsCommand>
{
    public CreateExamValiditior()
    {
        RuleFor(r => r.DueDate).NotEmpty().NotNull().Must(dueDate => dueDate > DateTime.UtcNow);
        RuleFor(r => r.HallNo).NotNull();
        RuleFor(r => r.Location).NotNull();

    }
}

