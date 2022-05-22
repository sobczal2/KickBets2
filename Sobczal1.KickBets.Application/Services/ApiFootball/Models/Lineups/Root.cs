using System.Text.Json.Serialization;
using Sobczal1.KickBets.Application.Services.ApiFootball.Models.Common;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Lineups;

public class Root
{
    [JsonPropertyName("team")]
    public JustId Team { get; set; } = null!;

    [JsonPropertyName("formation")]
    public string Formation { get; set; } = null!;

    [JsonPropertyName("startXI")]
    public List<PlayerWrapper> StartXI { get; set; } = null!;

    [JsonPropertyName("substitutes")]
    public List<PlayerWrapper> Substitutes { get; set; } = null!;

    [JsonPropertyName("coach")]
    public Coach Coach { get; set; } = null!;
}