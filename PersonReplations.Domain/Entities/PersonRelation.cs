namespace PersonReplations.Domain.Entities;

public class PersonRelation : EntityBase
{
  public int PersonId { get; set; }
  public Person Person { get; set; } = new();
  public int RelationId { get; set; }
  public Relation Relation { get; set; } = new();
}
