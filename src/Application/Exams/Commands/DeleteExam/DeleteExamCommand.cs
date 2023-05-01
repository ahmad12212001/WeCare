using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;

namespace WeCare.Application.Exams.Commands.DeleteCourse;
[Authorize(Roles = "AcademicStaff")]
public record DeleteExamCommand : IRequest<int>
{
    public int Id { get; set; } 
}
public class DeleteExamCommandHandler : IRequestHandler<DeleteExamCommand, int> {
    private readonly IApplicationDbContext _context;
    public DeleteExamCommandHandler(IApplicationDbContext applicationDbContext)
    {
        _context = applicationDbContext;
    }
    public async Task<int> Handle(DeleteExamCommand request, CancellationToken cancellationToken) 
    {
        var exam = (await _context.Exams.FindAsync(request.Id))!;
        await _context.SaveChangesAsync(cancellationToken);
        return exam.Id;
    }
}
