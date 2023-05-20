using System.Data;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Mappings;
using WeCare.Application.Common.Models;
using WeCare.Application.Requests.Dto;
using WeCare.Domain.Enums;

namespace WeCare.Application.Requests.Queries.GetDisabilityStudentRequests;
public record GetRequestsPaginationQuery : IRequest<PaginatedList<RequestDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}


public class GetRequestsPaginationQueryHandler : IRequestHandler<GetRequestsPaginationQuery, PaginatedList<RequestDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;

    public GetRequestsPaginationQueryHandler(IApplicationDbContext context, ICurrentUserService currentUserService, IIdentityService identityService)
    {
        _context = context;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }
    public async Task<PaginatedList<RequestDto>> Handle(GetRequestsPaginationQuery request, CancellationToken cancellationToken)
    {
        var userRole = await _identityService.GetUserRoleAsync(_currentUserService.UserId!);

        switch (userRole)
        {
            case "AcademicStaff":
                return await GetAcademicStaffRequestsAsync(request);
            case "VolunteerStudent":
                return await GetVolunteerRequestsAsync(request);
            case "DeanOffice":
                return await GetDeanOfficeRequestsAsync(request);
            case "DisabilityStudent":
                return await GetDisabilityStudentRequestsAsync(request);
        }

        return new PaginatedList<RequestDto>(new List<RequestDto>(), 0, 1, 10);
    }

    private async Task<PaginatedList<RequestDto>> GetAcademicStaffRequestsAsync(GetRequestsPaginationQuery request)
    {
        return await _context.Requests
          .Where(x => x.Course.UserId == _currentUserService.UserId!).OrderByDescending(i => i.DueDate)
          .Select(request => new RequestDto
          {
              CourseName = request.Course.Name,
              ExamName = request.Exam != null ? request.Exam.Name : string.Empty,
              RequestStatus = request.RequestStatus.ToString(),
              RequestType = request.RequestType.ToString(),
              Id = request.Id,
              DueDate = request.DueDate,
              VolunteerName = request.AssignedVolunteerStudent != null ?
              ($"{request.AssignedVolunteerStudent.User.FirstName} {request.AssignedVolunteerStudent.User.LastName}")
              : string.Empty,
              DisabilityName = ($"{request.DisabilityStudent.User.FirstName} {request.DisabilityStudent.User.FirstName}"),
              Description = request.Description,
              HasFeedback = request.FeedBacks != null && request.FeedBacks.Count() > 0,
          }).PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private async Task<PaginatedList<RequestDto>> GetDeanOfficeRequestsAsync(GetRequestsPaginationQuery request)
    {
        return await _context.Requests.OrderByDescending(i => i.DueDate)
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
              DisabilityName = ($"{request.DisabilityStudent.User.FirstName} {request.DisabilityStudent.User.LastName}"),
              Description = request.Description,
              HasFeedback = request.FeedBacks != null && request.FeedBacks.Count() > 0,
          }).PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private async Task<PaginatedList<RequestDto>> GetVolunteerRequestsAsync(GetRequestsPaginationQuery request)
    {
        var student = await _context.VolunteerStudents.SingleAsync(x => x.UserId == _currentUserService!.UserId);

        return await _context.Requests.Where(i =>
        (i.RequestType == RequestType.Assistance ? i.Exam!.Course.MajorGroupId != student.Major.MajorGroupId : true) &&
        ((i.RequestType == RequestType.Material || i.RequestType == RequestType.Assignment) ?
          i.Course.MajorGroupId == student.Major.MajorGroupId : false) &&
          (i.AssignedVolunteerStudentId == null || i.AssignedVolunteerStudentId == student.Id) &&
          (i.RequestStatus != RequestStatus.Done || (i.FeedBacks != null && i.FeedBacks.Any(f => f.StudentId == student.Id)))
          ).OrderByDescending(i => i.DueDate)
          .Select(request => new RequestDto
          {
              CourseName = request.Course.Name,
              ExamName = request.Exam != null ? request.Exam.Name : string.Empty,
              RequestStatus = request.RequestStatus.ToString(),
              RequestType = request.RequestType.ToString(),
              Id = request.Id,
              DueDate = request.DueDate,
              StudentName = ($"{request.DisabilityStudent.User.FirstName} {request.DisabilityStudent.User.LastName}"),
              Description = request.Description,
              HasRequested = request.Volunteers != null && request.Volunteers.Any(r => r.VolunteerStudentId == student.Id),
              HasFeedback = request.FeedBacks != null && request.FeedBacks.Any(f=> f.SubmitedByStudentId == student.Id)
          }).PaginatedListAsync(request.PageNumber, request.PageSize);
    }

    private async Task<PaginatedList<RequestDto>> GetDisabilityStudentRequestsAsync(GetRequestsPaginationQuery request)
    {
        var student = await _context.DisabilityStudents.SingleAsync(x => x.UserId == _currentUserService!.UserId);

        return await _context.Requests
           .Where(x => x.DisabilityStudentId == student.Id &&
                   (x.RequestStatus != RequestStatus.Done ||
                   (x.FeedBacks != null && x.FeedBacks.Any(f => f.StudentId == student.Id)))).OrderByDescending(i => i.DueDate)
           .Select(request => new RequestDto
           {
               CourseName = request.Course.Name,
               ExamName = request.Exam != null ? request.Exam.Name : string.Empty,
               RequestStatus = request.RequestStatus.ToString(),
               RequestType = request.RequestType.ToString(),
               Id = request.Id,
               DueDate = request.DueDate,
               StudentName = request.AssignedVolunteerStudent != null ?
               ($"{request.AssignedVolunteerStudent.User.FirstName} {request.AssignedVolunteerStudent.User.LastName}") : string.Empty,
               Description = request.Description,
               HasFeedback = request.FeedBacks != null && request.FeedBacks.Any(f => f.SubmitedByStudentId == student.Id)
           }).PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}