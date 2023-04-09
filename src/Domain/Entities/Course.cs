namespace WeCare.Domain.Entities;
public class Course : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public ApplicationUser User { get; set; } = null!;
    public ICollection<Material>? Materials { get; set; }
    public ICollection<StudentCourse>? Students { get; set; }
}
