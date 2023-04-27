using FluentValidation;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.RequestVolunteers.Commands.CreateRequestVolunteer;

namespace WeCare.Application.Requests.Commands.CreateRequestVolunteer;
public class RequestVolunteerCommandValidator : AbstractValidator<RequestVolunteerCommand>
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
