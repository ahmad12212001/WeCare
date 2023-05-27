using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;

namespace WeCare.Application.RequestFeedbacks.Commands.CreateRequestFeedback;
public record CreateRequestFeedbackCommand : IRequest<int>
{
    public int RequestId { get; set; }
    public string Comment { get; set; } = null!;

    public decimal Rate { get; set; }
}

public class CreateRequestFeedbackCommandHandler : IRequestHandler<CreateRequestFeedbackCommand, int>
{
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    private readonly IApplicationDbContext _context;
    public CreateRequestFeedbackCommandHandler(ICurrentUserService currentUserService,
        IApplicationDbContext context,
        IIdentityService identityService)
    {
        _currentUserService = currentUserService;
        _context = context;
        _identityService = identityService;

    }

    public async Task<int> Handle(CreateRequestFeedbackCommand request, CancellationToken cancellationToken)
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
                currentRequest.Rate = request.Rate;
                _context.Requests.Update(currentRequest);
                break;
        }

        var requestFeedBack = new RequestFeedBack
        {
            Comment = request.Comment,
            Rate = request.Rate,
            RequestId = request.RequestId,
            StudentId = student!.Id,
        };


        student.TotalRequest += 1;
        student.Rate = (student.Rate + request.Rate) / student.TotalRequest;

        _context.Students.Update(student);

        await _context.RequestFeedBacks.AddAsync(requestFeedBack);

        await _context.SaveChangesAsync(cancellationToken);

        return requestFeedBack.Id;
    }
}
