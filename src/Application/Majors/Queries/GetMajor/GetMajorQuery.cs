using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Security;
using WeCare.Application.Majors.Dtos;

namespace WeCare.Application.Majors.Queries.GetMajor;
[Authorize]
public record GetMajorQuery : IRequest<MajorDto>
{
    public int MajorId { get; set; }
}

public class GetMajorQueryHandler : IRequestHandler<GetMajorQuery, MajorDto>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMajorQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<MajorDto> Handle(GetMajorQuery request, CancellationToken cancellationToken)
    {
        var major = _context.Majors.AsNoTracking().Single(t => t.Id == request.MajorId);
        return _mapper.Map<MajorDto>(major);
    }
}
