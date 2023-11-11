using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence.DbConfig.Seed;

internal class RelationTypeSeed : IEntityTypeConfiguration<RelationType>
{
  public void Configure(EntityTypeBuilder<RelationType> builder)
  {
    builder.HasData(
      new RelationType()
      {
        Id = 1,
        DisplayName = "კოლეგა"
      },
      new RelationType()
      {
        Id = 2,
        DisplayName = "ნაცნობი"
      },
      new RelationType()
      {
        Id = 3,
        DisplayName = "ნათესავი"
      },
      new RelationType()
      {
        Id = 4,
        DisplayName = "სხვა"
      }
    );
  }
}
