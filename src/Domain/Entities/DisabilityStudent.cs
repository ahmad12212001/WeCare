namespace WeCare.Domain.Entities;
public class DisabilityStudent : Student
{
    public virtual ICollection<Request>? Requests { get; set; }
}
