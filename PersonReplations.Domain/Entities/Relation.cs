using PersonReplations.Domain.Entities.Abstraction;

namespace PersonReplations.Domain.Entities;

public class Relation : EntityBase
{
  public int RelationTypeId { get; set; }
  public RelationType? RelationType { get; set; }
  public IEnumerable<PersonRelation> PersonRelations { get; set; } = new List<PersonRelation>();

  public override void Deactivate()
  {
    IsActive = false;
    foreach (PersonRelation relation in PersonRelations)
    {
      relation.Deactivate();
    }
  }
}
