using Sobczal1.KickBets.Domain.Bets;

namespace Sobczal1.KickBets.Application.Contracts.Persistence.Bet;

public interface IBetsDataRepository
{
    Task<BetsData?> Get(int id);
}