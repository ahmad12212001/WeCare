using Microsoft.AspNetCore.Identity;

namespace WeCare.Domain.Entities;
public class ApplicationUser : IdentityUser
{
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;

    public bool IsActive { get; set; } = false;
}
