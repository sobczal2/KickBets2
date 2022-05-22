using System.Text.Json.Serialization;

namespace Sobczal1.KickBets.Application.Services.ApiFootball.Models.Common;

public class Result<TResult, TParameters>
{
    [JsonPropertyName("get")]
    public string Get { get; set; } = null!;

    [JsonPropertyName("parameters")]
    public TParameters? Parameters { get; set; }

    [JsonPropertyName("errors")]
    public List<object> Errors { get; set; } = null!;

    [JsonPropertyName("results")]
    public int Results { get; set; }

    [JsonPropertyName("paging")]
    public Paging Paging { get; set; } = null!;

    [JsonPropertyName("response")]
    public List<TResult> Response { get; set; } = null!;
}