using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Majors.Dtos;

namespace WeCare.Application.Majors.Queries.GetMajors;
public record GetMajorsListQuery : IRequest<List<MajorDto>>
{
}

public class GetMajorsListQueryHandler : IRequestHandler<GetMajorsListQuery, List<MajorDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    private readonly IMapper _mapper;
    public GetMajorsListQueryHandler(IApplicationDbContext applicationDbContext, IMapper mapper)
    {
        _applicationDbContext = applicationDbContext;
        _mapper = mapper;
    }

    public async Task<List<MajorDto>> Handle(GetMajorsListQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Majors
          .OrderBy(x => x.Name)
          .ProjectTo<MajorDto>(_mapper.ConfigurationProvider).ToListAsync(cancellationToken);


    }
}