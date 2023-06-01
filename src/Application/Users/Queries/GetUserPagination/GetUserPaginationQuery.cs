using System.Data;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Mappings;
using WeCare.Application.Common.Models;
using WeCare.Application.Users.Dtos;
using WeCare.Domain.Entities;

namespace WeCare.Application.Users.Queries.GetUserPagination;
public record GetUserPaginationQuery : IRequest<PaginatedList<ApplicationUserDto>>
{
    public string? Name { get; set; }
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}

public class GetUserPaginationQueryHandler : IRequestHandler<GetUserPaginationQuery, PaginatedList<ApplicationUserDto>>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _applicationDbContext;
    public GetUserPaginationQueryHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext applicationDbContext)
    {
        _userManager = userManager;
        _applicationDbContext = applicationDbContext;
    }
    public async Task<PaginatedList<ApplicationUserDto>> Handle(GetUserPaginationQuery request, CancellationToken cancellationToken)
    {

        var users = await _userManager.Users.
            Where(i => !string.IsNullOrEmpty(request.Name) ? i.FirstName.Contains(request.Name) : true)
            .PaginatedListAsync(request.PageNumber, request.PageSize);

        var usersDto = new List<ApplicationUserDto>();

        foreach (var user in users.Items)
        {
            ApplicationUserDto userDto = null;
            var role = (await (_userManager.GetRolesAsync(user))).FirstOrDefault();
            if (role != null && (role == "DisabilityStudent" || role == "VolunteerStudent"))
            {
                var student = await _applicationDbContext.Students.Where(i => i.UserId == user.Id).FirstOrDefaultAsync();

                userDto = new ApplicationUserDto
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Major = student?.Major?.Name,
                    PhoneNumber = user.PhoneNumber,
                    StudentId = student?.StudentId,
                    Id = user.Id,
                    Role = role
                };
            }
            else
            {
                userDto = new ApplicationUserDto
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    Id = user.Id,
                    Role = role
                };
            }

            usersDto.Add(userDto);
        }

        var uersDto = new PaginatedList<ApplicationUserDto>(usersDto, users.TotalPages, users.PageNumber, users.PageSize, users.TotalCount);
        return uersDto;
    }
}