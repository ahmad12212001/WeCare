namespace WeCare.Domain.Entities;
public class Major : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public int MajorGroupId { get; set; }
    public virtual MajorGroup MajorGroup { get; set; } = null!;
    public virtual ICollection<Student>? Students { get; set; }
}
