namespace WeCare.Application.Requests.Dto;
public class RequestDto
{
    public string CourseName { get; set; } = null!;

    public int Id { get; set; }

    public string? ExamName { get; set; }

    public string RequestType { get; set; } = null!;

    public string RequestStatus { get; set; } = null!;

    public DateTime DueDate { get; set; }

    public string? MaterialName { get; set; }

    public string? VolunteerName { get; set; }

    public string Description { get; set; } = null!;
}
