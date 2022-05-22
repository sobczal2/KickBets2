using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Sobczal1.KickBets.Application.DTOs;

public class BaseDto
{
    [JsonPropertyOrder(-1)]
    public int Id { get; set; }
}