namespace WeCare.Domain.Entities;
public class DisabilityStudent : Student
{
    public ICollection<Request>? Requests { get; set; }
}
