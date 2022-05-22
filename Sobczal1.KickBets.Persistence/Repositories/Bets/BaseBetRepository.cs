using Sobczal1.KickBets.Application.Contracts.Persistence.Bet;
using Sobczal1.KickBets.Domain.Bets;

namespace Sobczal1.KickBets.Persistence.Repositories.Bets;

public class BaseBetRepository : GenericRepository<BaseBet>, IBaseBetRepository
{
    public BaseBetRepository(KickBetsDbContext dbContext) : base(dbContext)
    {
    }
}