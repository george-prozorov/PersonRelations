using PersonReplations.Domain.Entities.Abstraction;

namespace PersonReplations.Domain.Entities;

public class Contact : EntityBase
{
  public int ContactTypeId { get; set; }
  public ContactType? ContactType { get; set; }
  public string Value { get; set; } = string.Empty;
}
