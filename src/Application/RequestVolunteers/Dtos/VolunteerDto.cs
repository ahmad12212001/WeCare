namespace WeCare.Application.RequestVolunteers.Dtos;
public class VolunteerDto
{
    public int Id { get; set; } 

    public string Name { get; set; } = null!;

    public decimal? Rating { get; set; }

    public string Major { get; set; } = null!;

}
