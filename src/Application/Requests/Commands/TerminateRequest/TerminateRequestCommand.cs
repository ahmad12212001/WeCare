using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Enums;

namespace WeCare.Application.Requests.Commands.TerminateRequest;
public record TerminateRequestCommand : IRequest<bool>
{
    public int RequestId { get; set; }
}

public class TerminateRequestCommandHandler : IRequestHandler<TerminateRequestCommand, bool>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;
    public TerminateRequestCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;

    }

    public async Task<bool> Handle(TerminateRequestCommand request, CancellationToken cancellationToken)
    {
        var currentRequest = (await _applicationDbContext.Requests.FindAsync(request.RequestId))!;

        var hasVolunteerFeedback = await _applicationDbContext.RequestFeedBacks.AnyAsync(i => i.SubmitedByStudentId == currentRequest.AssignedVolunteerStudentId);
        var hasDisabilityFeedback = await _applicationDbContext.RequestFeedBacks.AnyAsync(i => i.SubmitedByStudentId == currentRequest.DisabilityStudentId);
        if (hasDisabilityFeedback && hasDisabilityFeedback && currentRequest.CreatedBy == _currentUserService.UserId!)
        {
            currentRequest.RequestStatus = RequestStatus.Done;
            _applicationDbContext.Requests.Update(currentRequest);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

            return true;
        }

        return false;

    }
}
