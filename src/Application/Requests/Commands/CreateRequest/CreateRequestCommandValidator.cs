using FluentValidation;

namespace WeCare.Application.Requests.Commands.CreateRequest;
public class CreateRequestCommandValidator : AbstractValidator<CreateRequestCommand>
{
    public CreateRequestCommandValidator()
    {
        RuleFor(r => r.DueDate).NotEmpty().NotNull().Must(dueDate => dueDate > DateTime.UtcNow);
        RuleFor(r => r.RequestType).NotNull();
        RuleFor(r => r.ExamId).NotNull().When(r => r.RequestType == 0);
    }
}
