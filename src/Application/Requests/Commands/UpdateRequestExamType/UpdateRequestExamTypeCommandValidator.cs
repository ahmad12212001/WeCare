using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;

namespace WeCare.Application.Requests.Commands.UpdateRequestExamType;
public class RequestVolunteerCommandValidator : AbstractValidator<UpdateRequestExamTypeCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public RequestVolunteerCommandValidator(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;

        RuleFor(r => r.RequestId).NotEmpty().NotNull().Must(IsRequestExist);
    }

    private bool IsRequestExist(int requestId)
    {
        return _applicationDbContext.Requests.AsNoTracking().Any(c => c.Id == requestId);
    }
}
