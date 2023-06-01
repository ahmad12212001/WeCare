using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;

namespace WeCare.Application.MajorGroups.Queries.GetMajorGroups;
public class GetMajorGroupsQuery : IRequest<List<MajorGroup>>
{
}

public class GetMajorGroupsQueryHandler : IRequestHandler<GetMajorGroupsQuery, List<MajorGroup>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    public GetMajorGroupsQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<List<MajorGroup>> Handle(GetMajorGroupsQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.MajorGroups.ToListAsync(cancellationToken);

    }
}