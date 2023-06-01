using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;

namespace WeCare.Application.MajorGroups.Commands.DeleteMajorGroup;
public record DeleteMajorGroupCommand : IRequest<MajorGroup>
{
    public int Id { get; set; }
}

public class DeleteMajorGroupCommandHandler : IRequestHandler<DeleteMajorGroupCommand, MajorGroup>
{
    private readonly IApplicationDbContext _applicationDbContext;
    public DeleteMajorGroupCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<MajorGroup> Handle(DeleteMajorGroupCommand request, CancellationToken cancellationToken)
    {
        var majorGroup = await _applicationDbContext.MajorGroups.FindAsync(request.Id, cancellationToken);
        if (majorGroup == null)
        {
            return null;
        }

        _applicationDbContext.MajorGroups.Remove(majorGroup);
        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return majorGroup;
    }
}
