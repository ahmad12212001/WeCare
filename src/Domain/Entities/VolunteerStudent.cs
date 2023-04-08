namespace WeCare.Domain.Entities;
public class VolunteerStudent : Student
{
    public decimal? Rate { get; set; }
    public ICollection<RequestVolunteer>? Requests { get; set; }

}
