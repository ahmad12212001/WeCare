namespace WeCare.Domain.Entities;
public class Course : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    public string UserId { get; set; } = null!;
    public int? StudentId { get; set; }

    public ApplicationUser User { get; set; } = null!;
    public Student? Student { get; set; }
    public ICollection<Material>? Materials { get; set; }
}
