using WeCare.Application.Common.Models;
using WeCare.Domain.Entities;

namespace WeCare.Application.Common.Interfaces;
public interface IIdentityService
{
    Task<string?> GetUserNameAsync(string userId);

    Task<bool> IsInRoleAsync(string userId, string role);
    Task<string?> GetUserRoleAsync(string userId);

    Task<bool> AuthorizeAsync(string userId, string policyName);

    Task<(Result Result, string UserId)> CreateUserAsync(string userName, string password);

    Task<Result> DeleteUserAsync(string userId);

    Task<IList<ApplicationUser>> GetUsersInRoleAsync(string roleName);
}
