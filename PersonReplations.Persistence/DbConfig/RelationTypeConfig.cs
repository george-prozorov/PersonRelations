using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence.DbConfig;

internal class RelationTypeConfig : IEntityTypeConfiguration<RelationType>
{
  public void Configure(EntityTypeBuilder<RelationType> builder)
  {
    builder.Property(p => p.DisplayName)
      .HasMaxLength(50);
    builder.Property(p => p.IsActive)
      .IsRequired()
      .HasDefaultValue(true);
    builder.Property(p => p.IsActive)
      .IsRequired()
      .HasDefaultValue(DateTime.Now);
    builder
      .HasQueryFilter(p => p.IsActive == true);
  }
}
