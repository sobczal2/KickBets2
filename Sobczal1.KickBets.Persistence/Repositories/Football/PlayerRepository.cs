using Sobczal1.KickBets.Application.Contracts.Persistence.Football;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Persistence.Repositories.Football;

public class PlayerRepository : GenericRepository<Player>, IPlayerRepository
{
    public PlayerRepository(KickBetsDbContext dbContext) : base(dbContext)
    {
    }
}