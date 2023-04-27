using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;
using WeCare.Application.Exams.Dto;

namespace WeCare.Application.Exams.Queries.GetExam;
[Authorize]
public record GetExamQuery : IRequest<ExamDto>
{
    public string Name { get; set; } = null!;
}

public class GetExamQueryHandler : IRequestHandler<GetExamQuery, ExamDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetExamQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ExamDto> Handle(GetExamQuery request, CancellationToken cancellationToken)
    {
        var exam = _context.Exams.AsNoTracking().Single(t => t.Name == request.Name && t.DueDate >= DateTime.UtcNow);
        return _mapper.Map<ExamDto>(exam);
    }

}