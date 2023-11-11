using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence.DbConfig.Seed;

internal class GenderSeed : IEntityTypeConfiguration<Gender>
{
  public void Configure(EntityTypeBuilder<Gender> builder)
  {
    builder.HasData(
      new Gender()
      {
        Id = 1,
        DisplayName = "ქალი"
      },
      new Gender()
      {
        Id = 2,
        DisplayName = "კაცი"
      }
    );
  }
}
