namespace WeCare.Domain.Entities;
public class VolunteerStudent : Student
{
    public virtual ICollection<RequestVolunteer>? Requests { get; set; }

}
