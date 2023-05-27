namespace WeCare.Domain.Entities;
public class Student : BaseAuditableEntity
{
    public string StudentId { get; set; } = null!;
    public int MajorId { get; set; }
    public string UserId { get; set; } = null!;
    public virtual Major Major { get; set; } = null!;
    public virtual ApplicationUser User { get; set; } = null!;
    public decimal Rate { get; set; }
    public int TotalRequest { get; set; }
    public virtual ICollection<StudentCourse>? Courses { get; set; }
}
