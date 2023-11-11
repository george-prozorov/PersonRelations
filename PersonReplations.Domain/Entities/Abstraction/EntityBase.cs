namespace PersonReplations.Domain.Entities.Abstraction;

public abstract class EntityBase
{
    public int Id { get; set; }
    public bool? IsActive { get; set; }
    public DateTime CreatedAd { get; set; }
}
