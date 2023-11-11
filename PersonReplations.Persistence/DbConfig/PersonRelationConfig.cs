using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence.DbConfig;

public class PersonRelationConfig : IEntityTypeConfiguration<PersonRelation>
{
  public void Configure(EntityTypeBuilder<PersonRelation> builder)
  {
    builder
      .Property(p => p.IsActive)
      .IsRequired()
      .HasDefaultValue(true);
    builder
      .Property(p => p.CreatedAd)
      .HasDefaultValue("getdate()");
    builder
      .HasOne(p => p.Relation)
      .WithMany(p => p.PersonRelations)
      .HasForeignKey(p => p.RelationId)
      .OnDelete(DeleteBehavior.Restrict);
    builder
      .HasQueryFilter(p => p.IsActive == true);
  }
}
