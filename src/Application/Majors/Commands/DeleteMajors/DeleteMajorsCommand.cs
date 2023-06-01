using System.Data;
using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;
using WeCare.Domain.Entities;

namespace WeCare.Application.Majors.Commands.DeleteMajors;

[Authorize(Roles = "DeanOffice")]
public record DeleteMajorsCommand : IRequest<Major>
{
    public int MajorId { get; set; }


}
public class DeleteMajorsCommandHandler : IRequestHandler<DeleteMajorsCommand, Major> {
    private readonly IApplicationDbContext _context;
    public DeleteMajorsCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }
    public async Task<Major> Handle(DeleteMajorsCommand request, CancellationToken cancellationToken)
    {
        var major = (await _context.Majors.FindAsync(request.MajorId))/**/!;
        _context.Majors.Remove(major);
        await _context.SaveChangesAsync(cancellationToken);
        return major;
    }

}