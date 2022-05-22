namespace Sobczal1.KickBets.Application.DTOs.Football.Lineups;

public class LineupDto : BaseDto
{
    public int TeamId { get; set; }
    public string Formation { get; set; } = null!;
    public string CoachName { get; set; } = null!;
    public string CoachPhoto { get; set; } = null!;
}