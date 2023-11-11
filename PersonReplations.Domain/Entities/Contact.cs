namespace PersonReplations.Domain.Entities;

public class Contact : EntityBase
{
  public int ContactTypeId { get; set; }
  public Reference ContactType { get; set; } = new();
  public string Value { get; set; } = string.Empty;
}
