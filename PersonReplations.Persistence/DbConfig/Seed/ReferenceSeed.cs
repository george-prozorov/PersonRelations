using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonReplations.Domain;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence.DbConfig.Seed;

public class ReferenceSeed : IEntityTypeConfiguration<Reference>
{
  public void Configure(EntityTypeBuilder<Reference> builder)
  {
    builder.HasData(
      new Reference()
      {
        Id = 1,
        ReferenceTypeId = ReferenceTypes.Gender,
        DisplayName = "ქალი",
      },
      new Reference()
      {
        Id = 2,
        ReferenceTypeId = ReferenceTypes.Gender,
        DisplayName = "კაცი",
      },
      new Reference()
      {
        Id = 3,
        ReferenceTypeId = ReferenceTypes.ContactTypes,
        DisplayName = "მობილური",
      },
      new Reference()
      {
        Id = 4,
        ReferenceTypeId = ReferenceTypes.ContactTypes,
        DisplayName = "ოფისი",
      },
      new Reference()
      {
        Id = 5,
        ReferenceTypeId = ReferenceTypes.ContactTypes,
        DisplayName = "სახლი",
      },
      new Reference()
      {
        Id = 6,
        ReferenceTypeId = ReferenceTypes.RelationTypes,
        DisplayName = "კოლეგა",
      },
      new Reference()
      {
        Id = 7,
        ReferenceTypeId = ReferenceTypes.RelationTypes,
        DisplayName = "ნაცნობი",
      },
      new Reference()
      {
        Id = 8,
        ReferenceTypeId = ReferenceTypes.RelationTypes,
        DisplayName = "ნათესავი",
      },
      new Reference()
      {
        Id = 9,
        ReferenceTypeId = ReferenceTypes.RelationTypes,
        DisplayName = "სხვა",
      },
      new Reference()
      {
        Id = 10,
        ReferenceTypeId = ReferenceTypes.Cities,
        DisplayName = "თბილისი",
      },
      new Reference()
      {
        Id = 11,
        ReferenceTypeId = ReferenceTypes.Cities,
        DisplayName = "პრაღა",
      },
      new Reference()
      {
        Id = 12,
        ReferenceTypeId = ReferenceTypes.Cities,
        DisplayName = "პარიზი",
      }
    );
  }
}
