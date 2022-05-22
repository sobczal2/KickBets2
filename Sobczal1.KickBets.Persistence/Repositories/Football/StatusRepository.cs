using Sobczal1.KickBets.Application.Contracts.Persistence.Football;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Persistence.Repositories.Football;

public class StatusRepository : GenericRepository<Status>, IStatusRepository
{
    public StatusRepository(KickBetsDbContext dbContext) : base(dbContext)
    {
    }
}