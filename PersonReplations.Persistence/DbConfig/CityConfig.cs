using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence.DbConfig;

internal class CityConfig : IEntityTypeConfiguration<City>
{
  public void Configure(EntityTypeBuilder<City> builder)
  {
    builder
      .Property(p => p.DisplayName)
      .HasMaxLength(50);
    builder
      .Property(p => p.IsActive)
      .IsRequired()
      .HasDefaultValue(true);
    builder
      .Property(p => p.CreatedAd)
      .IsRequired()
      .HasDefaultValue("getdate()");
    builder
      .HasQueryFilter(p => p.IsActive == true);
  }
}
