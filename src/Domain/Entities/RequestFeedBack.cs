namespace WeCare.Domain.Entities;
public class RequestFeedBack : BaseAuditableEntity
{
    public string Comment { get; set; } = null!;
    public int StudentId { get; set; }
    public int RequestId { get; set; }
    public decimal Rate { get; set; }
    public virtual Student Student { get; set; } = null!;
    public virtual Request Request { get; set; } = null!;
}
