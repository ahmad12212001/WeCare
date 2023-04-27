using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;
using WeCare.Domain.Entities;

namespace WeCare.Application.Majors.Commands.CreateMajors;

[Authorize(Roles = "DeanOffice")]
public record CreateMajorCommand : IRequest<int>
{
    public string Name { get; set; } = null!;
}

public class CreateMajorCommandHandler : IRequestHandler<CreateMajorCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreateMajorCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }

    public async Task<int> Handle(CreateMajorCommand request, CancellationToken cancellationToken)
    {
        var major = new Major
        {
            Name = request.Name
        };

        await _context.Majors.AddAsync(major);

        await _context.SaveChangesAsync(cancellationToken);

        return major.Id;
    }
}