using WeCare.Application.Common.Mappings;
using WeCare.Domain.Entities;

namespace WeCare.Application.Majors.Dtos;
public class MajorDto : IMapFrom<Major>
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int MajorGroupId { get; set; }   
}
