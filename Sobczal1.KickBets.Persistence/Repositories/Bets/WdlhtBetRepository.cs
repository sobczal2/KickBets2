using Sobczal1.KickBets.Application.Contracts.Persistence.Bet;
using Sobczal1.KickBets.Domain.Bets;
using Sobczal1.KickBets.Domain.Bets.Wdlht;

namespace Sobczal1.KickBets.Persistence.Repositories.Bets;

public class WdlhtBetRepository : GenericRepository<WdlhtBet>, IWdlhtBetRepository
{
    public WdlhtBetRepository(KickBetsDbContext dbContext) : base(dbContext)
    {
    }
}