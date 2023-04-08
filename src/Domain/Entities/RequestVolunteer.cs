namespace WeCare.Domain.Entities;
public class RequestVolunteer : BaseAuditableEntity
{
    public int RequestId { get; set; }
    public int VolunteerStudentId { get; set; }
    public VolunteerStudent VolunteerStudent { get; set; } = null!;
    public Request Request { get; set; } = null!;
}
