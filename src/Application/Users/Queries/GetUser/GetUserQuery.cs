using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Users.Dtos;
using WeCare.Domain.Entities;

namespace WeCare.Application.Users.Queries.GetUser;
public record GetUserQuery : IRequest<ApplicationUserDto?>
{
    public string Id { get; set; } = null!;
}

public class GetUserQueryHandler : IRequestHandler<GetUserQuery, ApplicationUserDto?>
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IApplicationDbContext _applicationDbContext;
    public GetUserQueryHandler(UserManager<ApplicationUser> userManager, IApplicationDbContext applicationDbContext)
    {
        _userManager = userManager;
        _applicationDbContext = applicationDbContext;
    }
    public async Task<ApplicationUserDto?> Handle(GetUserQuery request, CancellationToken cancellationToken)
    {
        var user = await _userManager.FindByIdAsync(request.Id);
        if (user == null)
        {
            return null;
        }

        var role = (await _userManager.GetRolesAsync(user)).FirstOrDefault();

        if (role != null && (role == "DisabilityStudent" || role == "VolunteerStudent"))
        {
            var student = await _applicationDbContext.Students.Where(i => i.UserId == request.Id).FirstOrDefaultAsync();
            if (student == null)
            {
                return null;
            }

            return new ApplicationUserDto
            {
                Email = user.Email,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Major = student.Major.Name,
                PhoneNumber = user.PhoneNumber,
                StudentId = student.StudentId,
                Id = user.Id,
                Role = role
            };
        }

        return new ApplicationUserDto
        {
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            PhoneNumber = user.PhoneNumber,
            Id = user.Id,
            Role = role
        };

    }
}
