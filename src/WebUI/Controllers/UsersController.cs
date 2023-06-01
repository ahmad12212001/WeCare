using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Common.Interfaces;
using WeCare.Application.Common.Models;
using WeCare.Application.Users.Commands.ChangePassword;
using WeCare.Application.Users.Commands.CreateUser;
using WeCare.Application.Users.Commands.DeleteUser;
using WeCare.Application.Users.Commands.UpdateUser;
using WeCare.Application.Users.Dtos;
using WeCare.Application.Users.Queries.GetUser;
using WeCare.Application.Users.Queries.GetUserPagination;
using WeCare.Application.Users.Queries.GetUsersByRoleName;
using WeCare.Domain.Entities;
using WeCare.Infrastructure.Persistence;
using WeCare.WebUI.Controllers;

namespace WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ApiControllerBase
{

    private readonly ICurrentUserService _currentUserService;
    private readonly ApplicationDbContext _context;
    private readonly UserManager<ApplicationUser> _userManager;
    public UsersController(ApplicationDbContext context, ICurrentUserService currentUserService, UserManager<ApplicationUser> userManager)
    {
        _context = context;
        _currentUserService = currentUserService;
        _userManager = userManager;

    }

    [HttpGet("UserInfo")]
    public async Task<IActionResult> GetUserInfo()
    {
        var loggedInUser = (await _context.Set<ApplicationUser>().FindAsync(_currentUserService.UserId));

        return Ok(new
        {
            Username = $"{loggedInUser?.FirstName} {loggedInUser?.LastName}",
            Role = loggedInUser != null ? (await _userManager.GetRolesAsync(loggedInUser)).FirstOrDefault() : ""
        });
    }

    [HttpGet]
    public async Task<IList<ApplicationUser>> GetUsersByRole([FromQuery] GetUsersByRoleNameQuery getUsersByRoleNameQuery)
    {
        return await Mediator.Send(getUsersByRoleNameQuery);
    }

    [HttpGet("Pagination")]
    public async Task<PaginatedList<ApplicationUserDto>> GetUsersPagination([FromQuery] GetUserPaginationQuery getUsersByRoleNameQuery)
    {
        return await Mediator.Send(getUsersByRoleNameQuery);
    }

    [HttpGet("{id}")]
    public async Task<ApplicationUserDto?> GetUser(string id)
    {
        var getUserQuery = new GetUserQuery()
        {
            Id = id
        };

        return await Mediator.Send(getUserQuery);
    }

    [HttpPut]
    public async Task<ApplicationUserDto?> UpdateUser(UpdateUserCommand updateUserCommand)
    {
        return await Mediator.Send(updateUserCommand);
    }


    [HttpPut("ChangePassword")]
    public async Task<bool> ChangePassword(ChangePasswordCommand updateUserCommand)
    {
        return await Mediator.Send(updateUserCommand);
    }


    

    [HttpPost]
    public async Task<string> CreateUser(CreateUserCommand createUserCommand)
    {
        return await Mediator.Send(createUserCommand);
    }

    [HttpDelete("{id}")]
    public async Task<string> DeleteUser(DeleteUserCommand deleteUserCommand)
    {
        return await Mediator.Send(deleteUserCommand);
    }
}
