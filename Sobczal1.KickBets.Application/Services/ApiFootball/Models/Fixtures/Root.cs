using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Fixtures;

public class Root
{
    [JsonPropertyName("fixture")]
    public Fixture Fixture { get; set; } = null!;

    [JsonPropertyName("teams")]
    public Teams Teams { get; set; } = null!;

    [JsonPropertyName("goals")]
    public Goals Goals { get; set; } = null!;

    [JsonPropertyName("score")]
    public Score Score { get; set; } = null!;
}