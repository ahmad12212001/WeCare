namespace WeCare.Domain.Entities;
public class Major : BaseAuditableEntity
{
    public string Name { get; set; } = null!;

    public ICollection<Student>? Students { get; set; }
}
