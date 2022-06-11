using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Leagues;

public class Season
{
    [JsonPropertyName("year")] public int Year { get; set; }

    [JsonPropertyName("current")] public bool Current { get; set; }
}