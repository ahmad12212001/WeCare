using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;
using WeCare.Domain.Entities;
using WeCare.Domain.Enums;

namespace WeCare.Application.Requests.Commands.CreateRequest;

[Authorize(Roles = "DisabilityStudent")]
public record CreateRequestCommand : IRequest<int>
{
    public DateTime DueDate { get; set; }
    public RequestType RequestType { get; set; }

    public int? ExamId { get; set; }

    public int CourseId { get; set; }

}

public class CreateRequestCommandHandler : IRequestHandler<CreateRequestCommand, int>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;

    public CreateRequestCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
    }
    public async Task<int> Handle(CreateRequestCommand request, CancellationToken cancellationToken)
    {
        var student = await _applicationDbContext.DisabilityStudents.SingleAsync(x => x.UserId == _currentUserService!.UserId);

        var createdRequest = new Request
        {
            DueDate = request.DueDate,
            RequestType = request.RequestType,
            DisabilityStudentId = student?.Id!,
            CourseId = request.CourseId,
            ExamId = request.ExamId
        };
        await _applicationDbContext.Requests.AddAsync(createdRequest);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return createdRequest.Id;
    }
}
