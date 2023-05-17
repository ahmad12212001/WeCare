namespace WeCare.Domain.Entities;
public class Course : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public int MajorGroupId { get; set; }
    public string UserId { get; set; } = null!;
    public virtual ApplicationUser User { get; set; } = null!;
    public virtual ICollection<Material>? Materials { get; set; }
    public virtual ICollection<StudentCourse>? Students { get; set; }
    public virtual ICollection<Exam>? Exams { get; set; }
    public virtual MajorGroup MajorGroup { get; set; } = null!;
}
