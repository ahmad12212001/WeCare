using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;
using WeCare.Application.Exams.Dto;

namespace WeCare.Application.Exams.Queries.GetExam;

[Authorize]
public record GetExamByIdQuery : IRequest<ExamDto>
{
    public int Id { get; set; }
}

public class GetExamByIdQueryHandler : IRequestHandler<GetExamByIdQuery, ExamDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetExamByIdQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ExamDto> Handle(GetExamByIdQuery request, CancellationToken cancellationToken)
    {
        var exam = _context.Exams.AsNoTracking().Single(t => t.Id == request.Id);
        return _mapper.Map<ExamDto>(exam);
    }

}