namespace WeCare.Domain.Entities;
public class Student : BaseAuditableEntity
{
    public string StudentId { get; set; } = null!;
    public int MajorId { get; set; }
    public string UserId { get; set; } = null!;
    public Major Major { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    public ICollection<StudentCourse>? Courses { get; set; }
}
