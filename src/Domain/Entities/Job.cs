namespace WeCare.Domain.Entities;
public class Job : BaseEntity
{
    public string Name { get; set; } = null!;

    public string Description { get; set; } = null!;

    public string CronExpression { get; set; } = null!;

    public int Order { get; set; }
}
