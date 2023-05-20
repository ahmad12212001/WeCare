using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;

namespace WeCare.Application.RequestFeedbacks.Commands;
public record RequestFeedbackCommand : IRequest<int>
{
    public int RequestId { get; set; }
    public string Comment { get; set; } = null!;

    public decimal Rate { get; set; }
}

public class RequestFeedbackCommandHandler : IRequestHandler<RequestFeedbackCommand, int>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    public RequestFeedbackCommandHandler(ICurrentUserService currentUserService,
        IApplicationDbContext context,
        IIdentityService identityService)
    {
        _currentUserService = currentUserService;
        _context = context;
        _identityService = identityService;

    }

    public async Task<int> Handle(RequestFeedbackCommand request, CancellationToken cancellationToken)
    {
        var currentRequest = await _context.Requests.FindAsync(request.RequestId);
        Student student = null;

        var userRole = await _identityService.GetUserRoleAsync(_currentUserService.UserId!);


        switch (userRole)
        {
            case "VolunteerStudent":
                student = await _context.DisabilityStudents.FindAsync(currentRequest.DisabilityStudentId)!;
                break;
            case "DisabilityStudent":
                student = await _context.VolunteerStudents.FindAsync(currentRequest.AssignedVolunteerStudentId)!;
                break;
        }

        var requestFeedBack = new RequestFeedBack
        {
            Comment = request.Comment,
            Rate = request.Rate,
            RequestId = request.RequestId,
            StudentId = student.Id,
        };

        if (student.TotalRequest.HasValue)
        {
            student.TotalRequest += 1;
        }
        else
        {
            student.TotalRequest = 1;
        }

        if (student.Rate.HasValue)
        {
            student.Rate = (student.Rate + request.Rate) / student.TotalRequest;
        }
        else
        {
            student.Rate = request.Rate;
        }

        _context.Students.Update(student);

        await _context.RequestFeedBacks.AddAsync(requestFeedBack);

        await _context.SaveChangesAsync(cancellationToken);

        return requestFeedBack.Id;
    }
}
