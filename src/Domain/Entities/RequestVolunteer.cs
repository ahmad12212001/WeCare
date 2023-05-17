namespace WeCare.Domain.Entities;
public class RequestVolunteer : BaseAuditableEntity
{
    public int RequestId { get; set; }
    public int VolunteerStudentId { get; set; }
    public virtual VolunteerStudent VolunteerStudent { get; set; } = null!;
    public virtual Request Request { get; set; } = null!;
}
