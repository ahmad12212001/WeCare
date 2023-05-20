using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Mappings;
using WeCare.Application.Common.Models;
using WeCare.Application.Materials.Dto;
using WeCare.Domain.Enums;

namespace WeCare.Application.Materials.Queries.GetMaterials;
public record GetMaterialsQuery : IRequest<PaginatedList<MaterialDto>>
{
    public int PageSize { get; set; } = 10;

    public int PageNumber { get; set; } = 1;

    public int? RequestId { get; set; }
}


public class GetMaterialsQueryHandler : IRequestHandler<GetMaterialsQuery, PaginatedList<MaterialDto>>
{
    private readonly IApplicationDbContext _context;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    public GetMaterialsQueryHandler(ICurrentUserService currentUserService,
                                    IIdentityService identityService,
                                    IApplicationDbContext applicationDbContext)
    {
        _currentUserService = currentUserService;
        _context = applicationDbContext;
        _identityService = identityService;

    }
    public async Task<PaginatedList<MaterialDto>> Handle(GetMaterialsQuery request, CancellationToken cancellationToken)
    {
        var userRole = await _identityService.GetUserRoleAsync(_currentUserService.UserId!);

        switch (userRole)
        {
            case "AcademicStaff":
                return await GetMaterials(request, cancellationToken);
            case "VolunteerStudent":
            case "DisabilityStudent":
                return await GetStudentMaterials(request, cancellationToken);
        }

        return new PaginatedList<MaterialDto>(new List<MaterialDto>(), 0, 1, 10);
    }


    private async Task<PaginatedList<MaterialDto>> GetStudentMaterials(GetMaterialsQuery request, CancellationToken cancellationToken)
    {
        var courses = await _context.StudentCourses.Where(z => z.Student.UserId == _currentUserService.UserId!).Select(s => s.CourseId).ToListAsync(cancellationToken);

        return await _context.Materials.Where(m => courses.Contains(m.CourseId) &&
                                              m.MaterialStatus == MaterialStatus.Approved &&
                                            (request.RequestId.HasValue ? m.RequestId == request.RequestId : true))
            .Select(m => new MaterialDto
            {
                Id = m.Id,
                ContentType = m.ContentType,
                CourseId = m.CourseId,
                Description = m.Description,
                MaterialStatus = m.MaterialStatus,
                Name = m.Name,
                Path = m.Path,
                RequestId = m.RequestId,
                VolunteerStudentId = m.VolunteerStudentId,
                CourseName = m.Course.Name

            }).PaginatedListAsync(request.PageNumber, request.PageSize);

    }

    private async Task<PaginatedList<MaterialDto>> GetMaterials(GetMaterialsQuery request, CancellationToken cancellationToken)
    {
        return await _context.Materials.Where(m => m.Course.UserId == _currentUserService.UserId!)
           .Select(m => new MaterialDto
           {
               Id = m.Id,
               ContentType = m.ContentType,
               CourseId = m.CourseId,
               Description = m.Description,
               MaterialStatus = m.MaterialStatus,
               Name = m.Name,
               Path = m.Path,
               RequestId = m.RequestId,
               VolunteerStudentId = m.VolunteerStudentId,
               CourseName = m.Course.Name

           }).PaginatedListAsync(request.PageNumber, request.PageSize);

    }

}
