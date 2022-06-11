using System.Text.Json.Serialization;
using Sobczal1.KickBets.Application.Services.ApiFootball.Models.Common;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Lineups;

public class ColorsWrapper : JustId
{
    [JsonPropertyName("colors")] public Colors Colors { get; set; }
}