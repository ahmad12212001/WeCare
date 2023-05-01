namespace WeCare.Application.Common.Models;
public class EmailMessage
{
    public Dictionary<string, string> To { get; set; } = null!;
    public string Subject { get; set; } = null!;
    public string Content { get; set; } = null!;
}
