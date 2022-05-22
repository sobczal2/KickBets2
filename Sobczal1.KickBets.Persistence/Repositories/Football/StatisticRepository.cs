using Sobczal1.KickBets.Application.Contracts.Persistence.Football;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Persistence.Repositories.Football;

public class StatisticRepository : GenericRepository<Statistics>, IStatisticsRepository
{
    public StatisticRepository(KickBetsDbContext dbContext) : base(dbContext)
    {
    }
}