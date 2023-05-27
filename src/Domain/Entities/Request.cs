namespace WeCare.Domain.Entities;
public class Request : BaseAuditableEntity
{
    public string Description { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public RequestType RequestType { get; set; }
    public RequestStatus RequestStatus { get; set; }
    public decimal Rate { get; set; }
    public int CourseId { get; set; }
    public int? ExamId { get; set; }
    public int? DisabilityStudentId { get; set; }
    public int? AssignedVolunteerStudentId { get; set; }
    public virtual VolunteerStudent? AssignedVolunteerStudent { get; set; }
    public virtual DisabilityStudent DisabilityStudent { get; set; } = null!;
    public virtual Course Course { get; set; } = null!;
    public virtual Exam? Exam { get; set; }
    public virtual ICollection<Material>? Material { get; set; }
    public virtual ICollection<RequestFeedBack>? FeedBacks { get; set; }
    public virtual ICollection<RequestVolunteer>? Volunteers { get; set; }
}
