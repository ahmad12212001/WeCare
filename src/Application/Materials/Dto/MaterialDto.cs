using WeCare.Domain.Enums;

namespace WeCare.Application.Materials.Dto;
public class MaterialDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Path { get; set; } = null!;
    public string ContentType { get; set; } = null!;
    public int CourseId { get; set; }
    public string CourseName { get; set; } = null!;
    public string MaterialStatus { get; set; } = null!;
    public int? RequestId { get; set; }
    public int? VolunteerStudentId { get; set; }
}
