using MediatR;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Mappings;
using WeCare.Application.Common.Models;
using WeCare.Application.RequestVolunteers.Dtos;

namespace WeCare.Application.RequestVolunteers.Query.GetRequestVolunteers;
public record GetRequestVolunteerQuery : IRequest<PaginatedList<VolunteerDto>>
{
    public int Id { get; set; }
    public int PageSize { get; set; } = 10;
    public int PageNumber { get; set; } = 1;
}

public class GetRequestVolunteerQueryHandler : IRequestHandler<GetRequestVolunteerQuery, PaginatedList<VolunteerDto>>
{
    private readonly IApplicationDbContext _context;

    public GetRequestVolunteerQueryHandler(IApplicationDbContext context)
    {
        _context = context;

    }
    public async Task<PaginatedList<VolunteerDto>> Handle(GetRequestVolunteerQuery request, CancellationToken cancellationToken)
    {


        return await _context.RequestVolunteers
            .Where(x => x.RequestId == request.Id)
            .Select(request => new VolunteerDto
            {
                Id = request.VolunteerStudent.Id,
                Major = request.VolunteerStudent.Major.Name,
                Name = $"{request.VolunteerStudent.User.FirstName} {request.VolunteerStudent.User.LastName}",
                Rating = request.VolunteerStudent.Rate ?? 0

            }).PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}
