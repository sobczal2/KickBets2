namespace Sobczal1.KickBets.Application.DTOs.Football.Venues;

public class VenueDto : BaseDto
{
    public string Name { get; set; } = null!;
    public string? Address { get; set; }
    public string City { get; set; } = null!;
    public string Country { get; set; } = null!;
    public int Capacity { get; set; }
    public string Surface { get; set; } = null!;
    public string? Image { get; set; }
}