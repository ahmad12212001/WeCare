namespace WeCare.Domain.Entities;
public class MajorGroup : BaseAuditableEntity
{

    public string Name { get; set; } = null!;

    public virtual ICollection<Major> Majors { get; set; } = null!;
}
