using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Mappings;
using WeCare.Application.Common.Models;
using WeCare.Application.MajorGroups.Dtos;

namespace WeCare.Application.MajorGroups.Queries.GetMajorGroupsPagination;
public class GetMajorGroupsPaginationQuery : IRequest<PaginatedList<MajorGroupDto>>
{
    public int PageSize { get; set; } = 10;

    public int PageNumber { get; set; } = 1;

    public string? Name { get; set; }
}

public class GetMajorGroupsPaginationQueryHandler : IRequestHandler<GetMajorGroupsPaginationQuery, PaginatedList<MajorGroupDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;

    public GetMajorGroupsPaginationQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }
    public async Task<PaginatedList<MajorGroupDto>> Handle(GetMajorGroupsPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.MajorGroups.Where(m => !string.IsNullOrEmpty(request.Name) ? m.Name.Contains(request.Name) : true)
         .Select(m => new MajorGroupDto
         {
             Id = m.Id,
             Name = m.Name,
             Majors = m.Majors.Select(m => m.Name).ToList(),

         }).PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}