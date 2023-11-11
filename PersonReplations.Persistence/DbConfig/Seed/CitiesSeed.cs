using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence.DbConfig.Seed;

internal class CitiesSeed : IEntityTypeConfiguration<City>
{
  public void Configure(EntityTypeBuilder<City> builder)
  {
    builder.HasData(
      new City()
      {
        Id = 1,
        DisplayName = "თბილისი",
      },
      new City()
      {
        Id = 2,
        DisplayName = "პრაღა",
      },
      new City()
      {
        Id = 3,
        DisplayName = "პარიზი",
      }
    );
  }
}
