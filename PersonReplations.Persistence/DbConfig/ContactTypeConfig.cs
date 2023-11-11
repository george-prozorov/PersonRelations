using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence.DbConfig;

internal class ContactTypeConfig : IEntityTypeConfiguration<ContactType>
{
  public void Configure(EntityTypeBuilder<ContactType> builder)
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
      .HasColumnType("datetime")
      .HasDefaultValueSql("getdate()");
    builder
      .HasQueryFilter(p => p.IsActive == true);
  }
}
