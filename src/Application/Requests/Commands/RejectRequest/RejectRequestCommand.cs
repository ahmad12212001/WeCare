using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Enums;

namespace WeCare.Application.Requests.Commands.RejectRequest;

public record RejectRequestCommand : IRequest<bool>
{
    public int RequestId { get; set; }

    public int VolunteerStudentId { get; set; }
}

public class RejectRequestCommandHandler : IRequestHandler<RejectRequestCommand, bool>
{
    private readonly IApplicationDbContext _applicationDbContext;
    public RejectRequestCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;

    }

    public async Task<bool> Handle(RejectRequestCommand request, CancellationToken cancellationToken)
    {
        var currentRequest = (await _applicationDbContext.Requests.FindAsync(request.RequestId))!;

        if (currentRequest.AssignedVolunteerStudentId.HasValue && currentRequest.AssignedVolunteerStudentId == request.VolunteerStudentId)
        {
            currentRequest.RequestStatus = RequestStatus.ReAssigning;
            currentRequest.AssignedVolunteerStudentId = null;
            _applicationDbContext.Requests.Update(currentRequest);
            await _applicationDbContext.SaveChangesAsync(cancellationToken);

        }

        return true;
    }
}
