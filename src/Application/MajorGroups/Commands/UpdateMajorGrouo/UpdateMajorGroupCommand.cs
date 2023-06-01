using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;

namespace WeCare.Application.MajorGroups.Commands.UpdateMajorGrouo;
public record UpdateMajorGroupCommand : IRequest<MajorGroup>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

}

public class UpdateMajorGroupCommandHandler : IRequestHandler<UpdateMajorGroupCommand, MajorGroup?>
{
    private readonly IApplicationDbContext _context;

    public UpdateMajorGroupCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }
    public async Task<MajorGroup?> Handle(UpdateMajorGroupCommand request, CancellationToken cancellationToken)
    {
        var majorGroup = await _context.MajorGroups.FindAsync(request.Id, cancellationToken);
        if (majorGroup == null)
        {
            return null;
        }

        majorGroup.Name = request.Name;

        _context.MajorGroups.Update(majorGroup);
        await _context.SaveChangesAsync(cancellationToken);

        return majorGroup;
    }
}
