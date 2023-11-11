using PersonReplations.Domain.Entities.Abstraction;

namespace PersonReplations.Domain.Entities;

public class Contact : EntityBase
{
  public int ContactTypeId { get; set; }
  public ContactType ContactType { get; set; } = new();
  public string Value { get; set; } = string.Empty;
}
