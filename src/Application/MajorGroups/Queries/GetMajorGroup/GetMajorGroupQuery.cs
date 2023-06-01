using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;

namespace WeCare.Application.MajorGroups.Queries.GetMajorGroup;
public record GetMajorGroupQuery : IRequest<MajorGroup?>
{
    public int Id { get; set; }
}

public class GetMajorGroupQueryHandler : IRequestHandler<GetMajorGroupQuery, MajorGroup?>
{
    private readonly IApplicationDbContext _applicationDbContext;
    public GetMajorGroupQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<MajorGroup?> Handle(GetMajorGroupQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.MajorGroups.FindAsync(request.Id, cancellationToken);

    }
}