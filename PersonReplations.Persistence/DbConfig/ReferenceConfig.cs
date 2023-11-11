using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence.DbConfig;

internal class ReferenceConfig : IEntityTypeConfiguration<Reference>
{
  public void Configure(EntityTypeBuilder<Reference> builder)
  {
    builder.Property(p => p.DisplayName)
      .HasMaxLength(150);
    builder.Property(p => p.IsActive)
      .HasDefaultValue(true);
    builder.Property(p => p.CreatedAd)
      .HasDefaultValue(DateTime.Now);
  }
}
