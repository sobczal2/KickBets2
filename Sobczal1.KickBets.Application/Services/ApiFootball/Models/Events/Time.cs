using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Events;

public class Time
{
    [JsonPropertyName("elapsed")]
    public int? Elapsed { get; set; }

    [JsonPropertyName("extra")]
    public int? Extra { get; set; }
}