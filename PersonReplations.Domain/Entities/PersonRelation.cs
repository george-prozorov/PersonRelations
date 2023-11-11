using PersonReplations.Domain.Entities.Abstraction;

namespace PersonReplations.Domain.Entities;

public class PersonRelation : EntityBase
{
  public int PersonId { get; set; }
  public Person? Person { get; set; }
  public int RelationId { get; set; }
  public Relation? Relation { get; set; }
}
