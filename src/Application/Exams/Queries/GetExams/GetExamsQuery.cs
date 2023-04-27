using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Mappings;
using WeCare.Application.Common.Models;
using WeCare.Application.Common.Security;
using WeCare.Application.Exams.Dto;

namespace WeCare.Application.Exams.Queries.GetExams;
[Authorize]
public record GetExamsQuery : IRequest<PaginatedList<ExamDto>>

{
    public string? Name { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetExamsQueryHandler : IRequestHandler<GetExamsQuery, PaginatedList<ExamDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetExamsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    public async Task<PaginatedList<ExamDto>> Handle(GetExamsQuery request, CancellationToken cancellationToken)
    {

        return await _context.Exams.Where(x => !string.IsNullOrEmpty(request.Name) ? x.Name.Contains(request.Name) : true && x.DueDate >= DateTime.UtcNow)
              .OrderBy(x => x.Name)
              .ProjectTo<ExamDto>(_mapper.ConfigurationProvider)
              .PaginatedListAsync(request.PageNumber, request.PageSize);

    }

}
