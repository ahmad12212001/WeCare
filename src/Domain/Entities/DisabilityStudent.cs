namespace WeCare.Domain.Entities;
public class DisabilityStudent : Student
{
    public ICollection<Course>? Courses { get; set; } = null!;
    public ICollection<Request>? Requests { get; set; }
}
