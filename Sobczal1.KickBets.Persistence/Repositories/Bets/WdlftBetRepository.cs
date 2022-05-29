using Sobczal1.KickBets.Application.Contracts.Persistence.Bet;
using Sobczal1.KickBets.Domain.Bets;

namespace Sobczal1.KickBets.Persistence.Repositories.Bets;

public class WdlftBetRepository : GenericRepository<WdlftBet>, IWdlftBetRepository
{
    public WdlftBetRepository(KickBetsDbContext dbContext) : base(dbContext)
    {
    }
}