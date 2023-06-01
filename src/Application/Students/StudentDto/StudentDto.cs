namespace WeCare.Application.Students;
public class StudentDto
{
    public string Type { get; set; } = null!;
    public int Id { get; set; }
    public string StudentId { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string? PhoneNumber { get; set; }
    public string Major { get; set; } = null!;

    public string? Courses { get; set; }

}
