using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence.DbConfig;

internal class RelationConfig : IEntityTypeConfiguration<Relation>
{
  public void Configure(EntityTypeBuilder<Relation> builder)
  {
    builder
      .Property(p => p.IsActive)
      .IsRequired()
      .HasDefaultValue(true);
    builder
      .Property(p => p.CreatedAd)
      .HasDefaultValue(DateTime.Now);
    builder
      .HasQueryFilter(p => p.IsActive == true);
  }
}
