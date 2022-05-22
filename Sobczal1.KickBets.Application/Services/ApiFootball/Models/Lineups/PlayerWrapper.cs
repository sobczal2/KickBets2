using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Lineups;

public class PlayerWrapper
{
    [JsonPropertyName("player")]
    public Player Player { get; set; } = null!;
}