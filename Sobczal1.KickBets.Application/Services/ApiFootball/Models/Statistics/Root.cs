using System.Text.Json.Serialization;
using Sobczal1.KickBets.Application.Services.ApiFootball.Models.Common;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Statistics;

public class Root
{
    [JsonPropertyName("team")] public JustId Team { get; set; } = null!;

    [JsonPropertyName("statistics")] public List<Statistics> Statistics { get; set; } = null!;
}