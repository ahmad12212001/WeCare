namespace WeCare.Domain.Entities;
public class Material : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
    public ICollection<Document> Documents { get; set; } = null!;
}

