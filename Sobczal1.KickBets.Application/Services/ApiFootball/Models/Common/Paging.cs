using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Common;

public class Paging
{
    [JsonPropertyName("current")] public int Current { get; set; }

    [JsonPropertyName("total")] public int Total { get; set; }
}