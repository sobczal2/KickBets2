using Sobczal1.KickBets.Application.Contracts.Persistence.Football;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Persistence.Repositories.Football;

public class LineupRepository : GenericRepository<Lineup>, ILineupRepository
{
    public LineupRepository(KickBetsDbContext dbContext) : base(dbContext)
    {
    }
}