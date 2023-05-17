namespace WeCare.Domain.Entities;
public class Material : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Path { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public int CourseId { get; set; }
    public MaterialStatus MaterialStatus { get; set; }
    public int? RequestId { get; set; }
    public int? VolunteerStudentId { get; set; }
    public virtual Course Course { get; set; } = null!;
    public virtual VolunteerStudent? VolunteerStudent { get; set; }
    public virtual Request? Request { get; set; }

}

