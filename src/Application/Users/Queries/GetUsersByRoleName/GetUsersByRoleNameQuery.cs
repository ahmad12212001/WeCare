using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;

namespace WeCare.Application.Users.Queries.GetUsersByRoleName;
public record GetUsersByRoleNameQuery : IRequest<IList<ApplicationUser>>
{
    public string RoleName { get; set; } = null!;
}


public class GetUsersByRoleNameQueryHandler : IRequestHandler<GetUsersByRoleNameQuery, IList<ApplicationUser>>
{
    private readonly IIdentityService _identityService;

    public GetUsersByRoleNameQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }
    public async Task<IList<ApplicationUser>> Handle(GetUsersByRoleNameQuery request, CancellationToken cancellationToken)
    {
       return await _identityService.GetUsersInRoleAsync(request.RoleName);
    }
}