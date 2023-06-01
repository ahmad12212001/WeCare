using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Exams.Dto;

namespace WeCare.Application.Exams.Queries.GetExams;

public record GetExamsQuery : IRequest<List<ExamDto>>
{
}

public class GetExamsQueryHandler : IRequestHandler<GetExamsQuery, List<ExamDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetExamsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<List<ExamDto>> Handle(GetExamsQuery request, CancellationToken cancellationToken)
    {

        return await _context.Exams.Where(i => i.DueDate >= DateTime.UtcNow).ProjectTo<ExamDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);

    }

}
