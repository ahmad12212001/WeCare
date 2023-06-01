using WeCare.Application.Common.Mappings;
using WeCare.Domain.Entities;

namespace WeCare.Application.Courses.Dtos;
public class CourseDto : IMapFrom<Course>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string UserId { get; set; } = null!;
    public int MajorGroupId { get; set; }

    public string? AccadmeicStaffName { get; set; }

    public string? MajorGroupName { get; set; }

}
