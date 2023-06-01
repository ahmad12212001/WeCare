using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;

namespace WeCare.Application.MajorGroups.Commands.CreateMajorGroup;
public record CreateMajorGroupCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
}

public class CreateMajorGroupCommandHandler : IRequestHandler<CreateMajorGroupCommand, int>
{
    private readonly IApplicationDbContext _applicationDbContext;
    public CreateMajorGroupCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<int> Handle(CreateMajorGroupCommand request, CancellationToken cancellationToken)
    {
        var majorGroup = new MajorGroup
        {
            Name = request.Name
        };

        await _applicationDbContext.MajorGroups.AddAsync(majorGroup);

        await _applicationDbContext.SaveChangesAsync(cancellationToken);

        return majorGroup.Id;
    }
}
