namespace WeCare.Application.RequestFeedbacks.Dtos;
public class RequestFeedbackDto
{
    public int Id { get; set; }

    public string Comment { get; set; } = null!;

    public string StudentName { get; set; } = null!;

    public string SubmitedBy { get; set; } = null!;

    public decimal Rate { get; set; }
}
