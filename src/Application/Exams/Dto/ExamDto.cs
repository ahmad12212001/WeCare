using WeCare.Application.Common.Mappings;
using WeCare.Domain.Entities;

namespace WeCare.Application.Exams.Dto;
public class ExamDto : IMapFrom<Exam>
{
    public string Name { get; set; } = null!;
    public DateTime DueDate { get; set; }
    public string HallNo { get; set; } = null!;
    public string? Location { get; set; }
    public int CourseId { get; set; }
    public int Id { get; set; }
}
