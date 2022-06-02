namespace Sobczal1.KickBets.Application.DTOs.Football.Events;

public class EventDto
{
    public int? ElapsedTime { get; set; }
    public int? ExtraTime { get; set; }
    public int TeamId { get; set; }
    public string? PlayerName { get; set; }
    public string? AssistName { get; set; }
    public string Type { get; set; } = null!;
    public string? Detail { get; set; }
    public string? Comments { get; set; }
}