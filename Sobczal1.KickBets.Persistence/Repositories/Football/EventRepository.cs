using Sobczal1.KickBets.Application.Contracts.Persistence.Football;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Persistence.Repositories.Football;

public class EventRepository : GenericRepository<Event>, IEventRepository
{
    public EventRepository(KickBetsDbContext dbContext) : base(dbContext)
    {
    }
}