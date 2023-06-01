namespace WeCare.Application.MajorGroups.Dtos;
public class MajorGroupDto
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public List<string>? Majors { get; set; }
}
