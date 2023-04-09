using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Mappings;
using WeCare.Application.Common.Models;
using WeCare.Application.Common.Security;
using WeCare.Application.Majors.Dtos;

namespace WeCare.Application.Majors.Queries.GetMajor;
[Authorize]
public record GetMajorsQuery : IRequest<PaginatedList<MajorDto>>
{
    public string? Name { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetMajorsQueryHandler : IRequestHandler<GetMajorsQuery, PaginatedList<MajorDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMajorsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<PaginatedList<MajorDto>> Handle(GetMajorsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Majors
            .Where(x => !string.IsNullOrEmpty(request.Name) ? x.Name.Contains(request.Name) : true)
            .OrderBy(x => x.Name)
            .ProjectTo<MajorDto>(_mapper.ConfigurationProvider)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
 
    }
}
