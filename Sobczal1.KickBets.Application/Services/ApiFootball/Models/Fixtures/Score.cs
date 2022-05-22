using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Fixtures;

public class Score
{
    [JsonPropertyName("halftime")]
    public Goals Halftime { get; set; } = null!;

    [JsonPropertyName("fulltime")]
    public Goals Fulltime { get; set; } = null!;

    [JsonPropertyName("extratime")]
    public Goals Extratime { get; set; } = null!;

    [JsonPropertyName("penalty")]
    public Goals Penalty { get; set; } = null!;
}