namespace PersonReplations.Domain.Entities.Abstraction;

public abstract class Reference : EntityBase
{
  public string DisplayName { get; set; } = string.Empty;
}
