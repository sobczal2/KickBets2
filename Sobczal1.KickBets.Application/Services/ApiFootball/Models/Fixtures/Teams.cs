using System.Text.Json.Serialization;
using TeamsRoot = Sobczal1.KickBets.Application.Services.ApiFootball.Models.Teams.Root;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Fixtures;

public class Teams
{
    [JsonPropertyName("home")] public TeamsRoot Home { get; set; } = null!;

    [JsonPropertyName("away")] public TeamsRoot Away { get; set; } = null!;
}