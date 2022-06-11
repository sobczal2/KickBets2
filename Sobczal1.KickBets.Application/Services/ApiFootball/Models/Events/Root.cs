using System.Text.Json.Serialization;
using Sobczal1.KickBets.Application.Services.ApiFootball.Models.Common;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Events;

public class Root
{
    [JsonPropertyName("time")] public Time Time { get; set; } = null!;

    [JsonPropertyName("team")] public JustId Team { get; set; } = null!;

    [JsonPropertyName("player")] public Player Player { get; set; } = null!;

    [JsonPropertyName("assist")] public Player Assist { get; set; } = null!;

    [JsonPropertyName("type")] public string Type { get; set; } = null!;

    [JsonPropertyName("detail")] public string? Detail { get; set; }

    [JsonPropertyName("comments")] public string? Comments { get; set; }
}