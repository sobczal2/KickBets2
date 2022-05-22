namespace Sobczal1.KickBets.Application.DTOs.Football.Status;

public class StatusDto : BaseDto
{
    public string Long { get; set; } = null!;
    public string Short { get; set; } = null!;
    public int? Elapsed { get; set; }
}