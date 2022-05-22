using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Leagues;

public class Root
{
    [JsonPropertyName("league")]
    public League League { get; set; } = null!;

    [JsonPropertyName("country")]
    public Country Country { get; set; } = null!;
    
    [JsonPropertyName("seasons")]
    public List<Season> Seasons { get; set; } = null!;
}