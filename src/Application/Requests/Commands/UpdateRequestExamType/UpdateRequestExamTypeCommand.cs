using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;
using WeCare.Domain.Enums;

namespace WeCare.Application.Requests.Commands.UpdateRequestExamType;

[Authorize(Roles = "VolunteerStudent")]
public record UpdateRequestExamTypeCommand : IRequest
{
    public int RequestId { get; set; }
}

public class UpdateRequestExamTypeCommandHandler : IRequestHandler<UpdateRequestExamTypeCommand>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;

    public UpdateRequestExamTypeCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
    }
    public async Task<Unit> Handle(UpdateRequestExamTypeCommand request, CancellationToken cancellationToken)
    {
        var currentRequest = (await _applicationDbContext.Requests.FindAsync(request.RequestId))!;

        var student = await _applicationDbContext.VolunteerStudents.SingleAsync(s => s.UserId == _currentUserService.UserId!);

        currentRequest.RequestStatus = RequestStatus.Accepted;

        currentRequest.AssignedVolunteerStudentId = student.Id;

        _applicationDbContext.Requests.Update(currentRequest);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}