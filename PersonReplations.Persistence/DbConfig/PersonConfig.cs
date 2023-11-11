using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence.DbConfig;

public class PersonConfig : IEntityTypeConfiguration<Person>
{
  public void Configure(EntityTypeBuilder<Person> builder)
  {
    builder
      .Property(p => p.FirstName)
      .HasMaxLength(50);
    builder
      .Property(p => p.LastName)
      .HasMaxLength(50);
    builder
      .Property(p => p.PersonalId)
      .HasMaxLength(11);
    builder
      .Property(p => p.IsActive)
      .IsRequired()
      .HasDefaultValue(true);
    builder
      .Property(p => p.CreatedAd)
      .HasDefaultValue("getdate()");


    builder
      .HasQueryFilter(p => p.IsActive == true);


    builder.HasIndex(p => p.FirstName);
    builder.HasIndex(p => p.LastName);
    builder.HasIndex(p => p.PersonalId);
  }
}
