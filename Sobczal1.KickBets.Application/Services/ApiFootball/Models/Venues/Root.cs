using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Venues;

public class Root
{
    [JsonPropertyName("id")]
    public int Id { get; set; }

    [JsonPropertyName("name")]
    public string Name { get; set; } = null!;

    [JsonPropertyName("address")]
    public string Address { get; set; } = null!;

    [JsonPropertyName("city")]
    public string City { get; set; } = null!;

    [JsonPropertyName("country")]
    public string Country { get; set; } = null!;

    [JsonPropertyName("capacity")]
    public int Capacity { get; set; }

    [JsonPropertyName("surface")]
    public string Surface { get; set; } = null!;

    [JsonPropertyName("image")]
    public string? Image { get; set; } = null!;
}