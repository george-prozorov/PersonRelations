using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence.DbConfig;

public class ContactConfig : IEntityTypeConfiguration<Contact>
{
  public void Configure(EntityTypeBuilder<Contact> builder)
  {
    builder.Property(p => p.Value)
      .HasMaxLength(50);
    builder.Property(p => p.IsActive)
      .HasDefaultValue(true);
    builder.Property(p => p.CreatedAd)
      .HasDefaultValue(DateTime.Now);
  }
}
