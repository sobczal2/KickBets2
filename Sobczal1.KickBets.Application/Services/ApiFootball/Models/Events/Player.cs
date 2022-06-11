using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Events;

public class Player
{
    [JsonPropertyName("name")] public string? Name { get; set; } = null!;
}