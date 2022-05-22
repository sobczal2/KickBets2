using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Fixtures;

public class Status
{
    [JsonPropertyName("long")]
    public string Long { get; set; } = null!;

    [JsonPropertyName("short")]
    public string Short { get; set; } = null!;

    [JsonPropertyName("elapsed")]
    public int? Elapsed { get; set; }
}