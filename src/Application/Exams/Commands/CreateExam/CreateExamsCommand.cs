using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;
using WeCare.Domain.Entities;

namespace WeCare.Application.Exams.Commands.CreateExam;
[Authorize(Roles = "AcademicStaff")]


public record CreateExamsCommand : IRequest<int>
{
    public DateTime DueDate { get; set; }
    public string HallNo { get; set; } = null!;
    public string Location { get; set; } = null!;
    public int CourseId { get; set; }





}


public class CreateExamsCommandHandler : IRequestHandler<CreateExamsCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUser;

    public CreateExamsCommandHandler(IApplicationDbContext applicationDbContext, ICurrentUserService currentUserService)
    {
        _context = applicationDbContext;
        _currentUser = currentUserService;


    }

    public async Task<int> Handle(CreateExamsCommand request, CancellationToken cancellationToken)
    {
        var course = (await _context.Courses.FindAsync(request.CourseId))!;
        var createdExam = new Exam
        {
            DueDate = request.DueDate,
            HallNo = request.HallNo,
            Location = request.Location,
            CourseId = request.CourseId,
            Name = course.Name

        };
        await _context.Exams.AddAsync(createdExam);
        await _context.SaveChangesAsync(cancellationToken);
        return createdExam.Id;





    }
}