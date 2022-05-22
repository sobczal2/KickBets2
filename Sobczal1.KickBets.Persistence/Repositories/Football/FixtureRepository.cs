using Microsoft.EntityFrameworkCore;
using Sobczal1.KickBets.Application.Contracts.Persistence.Football;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Persistence.Repositories.Football;

public class FixtureRepository : GenericRepository<Fixture>, IFixtureRepository
{
    private readonly KickBetsDbContext _dbContext;

    public FixtureRepository(KickBetsDbContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

#pragma warning disable CS1998
    public async Task<IQueryable<Fixture>> GetAllWithStatus()
#pragma warning restore CS1998
    {
        return _dbContext.Fixtures.Include(f => f.Status);
    }

    public async Task<Fixture?> GetWithStatistics(int id)
    {
        return await _dbContext.Fixtures.Include(f => f.HomeStatistics).Include(f => f.HomeStatistics)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Fixture?> GetWithLineups(int id)
    {
        return await _dbContext.Fixtures.Include(f => f.HomeLineup).Include(f => f.AwayLineup)
            .FirstOrDefaultAsync(f => f.Id == id);
    }

    public async Task<Fixture?> GetWithEvents(int id)
    {
        return await _dbContext.Fixtures.Include(f => f.HomeEvents).Include(f => f.AwayEvents)
            .FirstOrDefaultAsync(f => f.Id == id);
    }
}