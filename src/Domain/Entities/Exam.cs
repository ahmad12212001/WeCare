namespace WeCare.Domain.Entities;
public class Exam : BaseAuditableEntity
{
    public string Name { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public string HallNo { get; set; } = null!;
    public string? Location { get; set; }
    public int CourseId { get; set; }
    public Course Course { get; set; } = null!;
}
