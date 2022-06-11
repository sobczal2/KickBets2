using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Fixtures;

public class Goals
{
    [JsonPropertyName("home")] public int? Home { get; set; }

    [JsonPropertyName("away")] public int? Away { get; set; }
}