﻿namespace Sobczal1.KickBets.Application.DTOs.Bets;

public class WdlhtBetsDataDto : BaseDto
{
    public double HomeBetsMultiplier { get; set; }
    public double DrawBetsMultiplier { get; set; }
    public double AwayBetsMultiplier { get; set; }
}