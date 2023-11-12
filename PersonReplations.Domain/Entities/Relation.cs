﻿using PersonReplations.Domain.Entities.Abstraction;

namespace PersonReplations.Domain.Entities;

public class Relation : EntityBase
{
  public int RelationTypeId { get; set; }
  public RelationType? RelationType { get; set; }
  public IEnumerable<PersonRelation> PersonRelations { get; set; } = new List<PersonRelation>();
}
