using MediatR;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Mappings;
using WeCare.Application.Common.Models;


namespace WeCare.Application.Students.Queries;
public record GetStudentsPaginationQuery : IRequest<PaginatedList<StudentDto>>
{
    public string? Name { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetStudentsPaginationQueryHandler : IRequestHandler<GetStudentsPaginationQuery, PaginatedList<StudentDto>>
{
    private readonly IApplicationDbContext _applicationDbContext;
    public GetStudentsPaginationQueryHandler(IApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<PaginatedList<StudentDto>> Handle(GetStudentsPaginationQuery request, CancellationToken cancellationToken)
    {
        return await _applicationDbContext.Students.Include(i => i.User).Include(i => i.Major)
            .Where(x => !string.IsNullOrEmpty(request.Name) ? x.User.FirstName.Contains(request.Name) : true)
            .Select(s => new StudentDto
            {
                Email = s.User.Email!,
                FirstName = s.User.FirstName,
                LastName = s.User.LastName,
                Id = s.Id,
                Major = s.Major.Name,
                PhoneNumber = s.User.PhoneNumber,
                StudentId = s.StudentId,
                Type = s.Discriminator,
                Courses = s.Courses != null ? string.Join(",", s.Courses.Select(i => i.Course.Name).ToList()) : "",
            }).OrderBy(x => x.StudentId)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

    }
}