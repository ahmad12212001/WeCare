using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Enums;

namespace WeCare.Application.Materials.Commands.ChangeMaterialStatus;
public record ChangeMaterialStatusCommand : IRequest<bool>
{
    public MaterialStatus MaterialStatus { get; set; }

    public int MaterialId { get; set; }
}

public class ChangeMaterialStatusCommandHandler : IRequestHandler<ChangeMaterialStatusCommand, bool>
{
    private readonly IApplicationDbContext _applicationDbContext;
    public ChangeMaterialStatusCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<bool> Handle(ChangeMaterialStatusCommand request, CancellationToken cancellationToken)
    {
        var material = (await _applicationDbContext.Materials.FindAsync(request.MaterialId))!;

        material.MaterialStatus = request.MaterialStatus;

        _applicationDbContext.Materials.Update(material);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}
