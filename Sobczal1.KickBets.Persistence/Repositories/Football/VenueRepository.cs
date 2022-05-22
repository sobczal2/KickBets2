using Sobczal1.KickBets.Application.Contracts.Persistence.Football;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Persistence.Repositories.Football;

public class VenueRepository : GenericRepository<Venue>, IVenueRepository
{
    public VenueRepository(KickBetsDbContext dbContext) : base(dbContext)
    {
    }
}