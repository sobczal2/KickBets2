namespace Sobczal1.KickBets.Application.DTOs.Football.Fixtures;

public class FixtureDto : BaseDto
{
    public string? Referee { get; set; }
    public DateTime Date { get; set; }
    public int? VenueId { get; set; }
    public int StatusId { get; set; }
    public int LeagueId { get; set; }
    public int? HomeTeamId { get; set; }
    public int? AwayTeamId { get; set; }
    public int? HomeLineupId { get; set; }
    public int? AwayLineupId { get; set; }
    public int ScoreId { get; set; }
    public int? HomeStatisticsId { get; set; }
    public int? AwayStatisticsId { get; set; }
    public int BetsDataId { get; set; }
}