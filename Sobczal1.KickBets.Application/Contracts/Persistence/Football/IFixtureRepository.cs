using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Application.Contracts.Persistence.Football;

public interface IFixtureRepository : IGenericRepository<Fixture>
{
    Task<IQueryable<Fixture>> GetAllWithStatus();
    Task<Fixture?> GetWithStatistics(int id);
    Task<Fixture?> GetWithLineups(int id);
    Task<Fixture?> GetWithEvents(int id);
}