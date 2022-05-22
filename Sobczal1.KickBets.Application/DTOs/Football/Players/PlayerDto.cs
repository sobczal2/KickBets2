namespace Sobczal1.KickBets.Application.DTOs.Football.Players;

public class PlayerDto : BaseDto
{
    public string Name { get; set; } = null!;
    public int Number { get; set; }
    public string Pos { get; set; } = null!;
    public int? GridX { get; set; }
    public int? GridY { get; set; }
    public bool Starting11 { get; set; }
}