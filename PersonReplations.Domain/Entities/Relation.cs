namespace PersonReplations.Domain.Entities;

public class Relation : EntityBase
{
  public int RelationTypeId { get; set; }
  public Reference RelationType { get; set; } = new();
  public IEnumerable<PersonRelation> PersonRelations { get; set; } = Enumerable.Empty<PersonRelation>();
}
