namespace WeCare.Application.Users.Dtos;
public class ApplicationUserDto
{
    public string Role { get; set; } = null!;
    public string Id { get; set; }
    public string StudentId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string Major { get; set; } = null!;
}
