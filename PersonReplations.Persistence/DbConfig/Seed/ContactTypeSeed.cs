using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence.DbConfig.Seed;

internal class ContactTypeSeed : IEntityTypeConfiguration<ContactType>
{
  public void Configure(EntityTypeBuilder<ContactType> builder)
  {
    builder.HasData(
      new ContactType()
      {
        Id = 1,
        DisplayName = "მობილური",
      },
      new ContactType()
      {
        Id = 2,
        DisplayName = "ოფისის",
      },
      new ContactType()
      {
        Id = 3,
        DisplayName = "სახლის",
      }
    );
  }
}
