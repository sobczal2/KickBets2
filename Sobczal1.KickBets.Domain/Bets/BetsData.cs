namespace Sobczal1.KickBets.Domain.Bets;

public class BetsData : BaseDomainEntity
{
    public BetsData()
    {
        WdlftBetsData = new WdlftBetsData();
        WdlhtBetsData = new WdlhtBetsData();
    }
    public int WdlftBetsDataId { get; set; }
    public WdlftBetsData WdlftBetsData { get; set; }
    public int WdlhtBetsDataId { get; set; }
    public WdlhtBetsData WdlhtBetsData { get; set; }
}