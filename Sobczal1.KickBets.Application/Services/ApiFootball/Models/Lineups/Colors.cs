using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Lineups;

public class Colors
{
    [JsonPropertyName("player")]
    public Color Player { get; set; } = null!;

    [JsonPropertyName("goalkeeper")]
    public Color Goalkeeper { get; set; } = null!;
}