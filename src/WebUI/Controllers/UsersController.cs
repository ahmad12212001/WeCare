using CommunityToolkit.HighPerformance.Helpers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WeCare.Application.Common.Interfaces;
using WeCare.Domain.Entities;
using WeCare.Infrastructure.Persistence;

namespace WebUI.Controllers;
[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
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
}
