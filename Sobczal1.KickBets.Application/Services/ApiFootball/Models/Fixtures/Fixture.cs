using System.Text.Json.Serialization;
using Sobczal1.KickBets.Application.Services.ApiFootball.Models.Common;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Fixtures;

public class Fixture
{
    [JsonPropertyName("id")] public int Id { get; set; }

    [JsonPropertyName("referee")] public string? Referee { get; set; }

    [JsonPropertyName("date")] public DateTime Date { get; set; }

    [JsonPropertyName("venue")] public JustId Venue { get; set; } = null!;

    [JsonPropertyName("status")] public Status Status { get; set; } = null!;
}