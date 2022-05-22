using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Lineups;

public class Color
{
    [JsonPropertyName("primary")]
    public string Primary { get; set; } = null!;

    [JsonPropertyName("number")]
    public string Number { get; set; } = null!;

    [JsonPropertyName("border")]
    public string Border { get; set; } = null!;
}