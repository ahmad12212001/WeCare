using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;
using WeCare.Application.Common.Interfaces;

namespace WeCare.Application.Students.Commands.CreateStudent;
public class CreateStudentCommandValidator : AbstractValidator<CreateStudentCommand>
{
    
    public CreateStudentCommandValidator(IApplicationDbContext applicationDbContext) {
  
        RuleFor(r => r.Major).NotEmpty().NotNull();
        RuleFor(r => r.Id).NotNull();





    }
}
