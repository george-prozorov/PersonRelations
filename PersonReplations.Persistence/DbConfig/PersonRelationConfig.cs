﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Persistence.DbConfig;

public class PersonRelationConfig : IEntityTypeConfiguration<PersonRelation>
{
  public void Configure(EntityTypeBuilder<PersonRelation> builder)
  {
    builder.Property(p => p.IsActive)
      .HasDefaultValue(true);
    builder.Property(p => p.CreatedAd)
      .HasDefaultValue(DateTime.Now);
    builder
      .HasOne(p => p.Relation)
      .WithMany(p => p.PersonRelations)
      .HasForeignKey(p => p.RelationId)
      .OnDelete(DeleteBehavior.Restrict);
  }
}
