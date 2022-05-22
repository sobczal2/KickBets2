namespace Sobczal1.KickBets.Application.DTOs.Football.Leagues;

public class LeagueDto : BaseDto
{
    public string Name { get; set; } = null!;
    public string Country { get; set; } = null!;
    public string? Logo { get; set; }
    public string? Flag { get; set; }
}