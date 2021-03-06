using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Lineups;

public class Player
{
    [JsonPropertyName("number")] public int Number { get; set; }

    [JsonPropertyName("name")] public string Name { get; set; } = null!;

    [JsonPropertyName("pos")] public string Pos { get; set; } = null!;

    [JsonPropertyName("grid")] public string Grid { get; set; } = null!;

    [JsonIgnore] public bool Starting11 { get; set; }
}