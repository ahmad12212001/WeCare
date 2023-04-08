namespace WeCare.Domain.Entities;
public class Request : BaseAuditableEntity
{
    public DateTime DueDate { get; set; }
    public RequestType RequestType { get; set; }
    public RequestStatus RequestStatus { get; set; }
    public decimal? Rate { get; set; }
    public int? CourseId { get; set; }
    public int? MaterialId { get; set; }
    public int? ExamId { get; set; }
    public int? DisabilityStudentId { get; set; }
    public int? AssignedVolunteerStudentId { get; set; }
    public VolunteerStudent? AssignedVolunteerStudent { get; set; }
    public DisabilityStudent DisabilityStudent { get; set; } = null!;
    public Course? Course { get; set; }
    public Material? Material { get; set; }
    public Exam? Exam { get; set; }
    public ICollection<RequestFeedBack>? FeedBacks { get; set; }
    public ICollection<RequestVolunteer>? Volunteers { get; set; }
}
