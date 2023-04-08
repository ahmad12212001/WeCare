namespace WeCare.Domain.Entities;
public class RequestFeedBack : BaseAuditableEntity
{
    public string Comment { get; set; } = null!;
    public int StudentId { get; set; }
    public int RequestId { get; set; }
    public Student Student { get; set; } = null!;
    public Request Request { get; set; } = null!;
}
