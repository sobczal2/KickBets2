using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sobczal1.KickBets.Domain.Football;

namespace Sobczal1.KickBets.Persistence.Configuration.Entities;

public class StatisticsConfiguration : IEntityTypeConfiguration<Statistics>
{
    public void Configure(EntityTypeBuilder<Statistics> builder)
    {
    }
}