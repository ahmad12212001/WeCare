using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;

namespace WeCare.Application.RequestVolunteers.Commands.CreateRequestVolunteer;
public record RequestVolunteerCommand : IRequest<int>
{
    public int RequestId { get; set; }
}


public class RequestVolunteerCommandHandler : IRequestHandler<RequestVolunteerCommand, int>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly ICurrentUserService _currentUserService;

    public RequestVolunteerCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _applicationDbContext = applicationDbContext;
        _currentUserService = currentUserService;
    }
    public async Task<int> Handle(RequestVolunteerCommand request, CancellationToken cancellationToken)
    {

        var student = await _applicationDbContext.VolunteerStudents.SingleAsync(s => s.UserId == _currentUserService.UserId!);

        var requestVolunteer = new RequestVolunteer
        {
            VolunteerStudentId = student.Id,
            RequestId = request.RequestId,
        };

      await  _applicationDbContext.RequestVolunteers.AddAsync(requestVolunteer);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return requestVolunteer.Id;
    }
}