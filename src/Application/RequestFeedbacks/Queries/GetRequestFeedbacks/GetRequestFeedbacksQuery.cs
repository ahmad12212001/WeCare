using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.RequestFeedbacks.Dtos;

namespace WeCare.Application.RequestFeedbacks.Queries.GetRequestFeedbacks;
public record GetRequestFeedbacksQuery : IRequest<List<RequestFeedbackDto>>
{

    public int RequestId { get; set; }
}

public class GetRequestFeedbacksQueryHandler : IRequestHandler<GetRequestFeedbacksQuery, List<RequestFeedbackDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    public GetRequestFeedbacksQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<List<RequestFeedbackDto>> Handle(GetRequestFeedbacksQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.RequestFeedBacks.Where(r => r.RequestId == request.RequestId)
            .Select(fe => new RequestFeedbackDto
            {
                Comment = fe.Comment,
                Id = fe.RequestId,
                Rate = fe.Rate,
                StudentName = $"{fe.Student.User.FirstName} {fe.Student.User.LastName}",
                SubmitedBy = $"{fe.SubmitedByStudent.User.FirstName} {fe.SubmitedByStudent.User.LastName}"
            }).ToListAsync();
    }
}
