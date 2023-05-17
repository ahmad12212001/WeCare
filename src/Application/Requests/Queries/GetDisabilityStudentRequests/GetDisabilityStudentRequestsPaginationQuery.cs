using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Mappings;
using WeCare.Application.Common.Models;
using WeCare.Application.Requests.Dto;

namespace WeCare.Application.Requests.Queries.GetDisabilityStudentRequests;
public record GetDisabilityStudentRequestsPaginationQuery : IRequest<PaginatedList<RequestDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetDisabilityStudentRequestsPaginationQueryHandler : IRequestHandler<GetDisabilityStudentRequestsPaginationQuery, PaginatedList<RequestDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;

    public GetDisabilityStudentRequestsPaginationQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService)
    {
        _context = context;
        _currentUserService = currentUserService;
    }

    public async Task<PaginatedList<RequestDto>> Handle(GetDisabilityStudentRequestsPaginationQuery request, CancellationToken cancellationToken)
    {
        var student = await _context.DisabilityStudents.SingleAsync(x => x.UserId == _currentUserService!.UserId);

        return await _context.Requests
            .Where(x => x.DisabilityStudentId == student.Id)
            .Select(request => new RequestDto
            {
                CourseName = request.Course.Name,
                ExamName = request.Exam != null ? request.Exam.Name : string.Empty,
                RequestStatus = request.RequestStatus.ToString(),
                RequestType = request.RequestType.ToString(),
                Id = request.Id,
                DueDate = request.DueDate,
                VolunteerName = request.AssignedVolunteerStudent != null ? 
                ($"{request.AssignedVolunteerStudent.User.FirstName} {request.AssignedVolunteerStudent.User.LastName}") : string.Empty,
                Description = request.Description,

            }).PaginatedListAsync(request.PageNumber, request.PageSize);

    }
}