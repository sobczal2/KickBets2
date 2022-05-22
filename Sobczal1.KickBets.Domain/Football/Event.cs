namespace Sobczal1.KickBets.Domain.Football;

public class Event : BaseDomainEntity
{
    public int? ElapsedTime { get; set; }
    public int? ExtraTime { get; set; }
    public int TeamId { get; set; }
    public virtual Team Team { get; set; } = null!;
    public string? PlayerName { get; set; } = null!;
    public string? AssistName { get; set; }
    public string Type { get; set; } = null!;
    public string? Detail { get; set; }
    public string? Comments { get; set; }
    public int FixtureId { get; set; }
    public virtual Fixture Fixture { get; set; } = null!;
}