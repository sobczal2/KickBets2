using Sobczal1.KickBets.Domain.Bets.Wdlft;
using Sobczal1.KickBets.Domain.Bets.Wdlht;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Domain.Bets;

public class BetsData : BaseDomainEntity
{
    public BetsData()
    {
        WdlftData = new WdlftData();
        WdlhtData = new WdlhtData();
    }
    public WdlftData WdlftData { get; set; }
    public WdlhtData WdlhtData { get; set; }
    public int FixtureId { get; set; }
    public Fixture Fixture { get; set; } = null!;
}