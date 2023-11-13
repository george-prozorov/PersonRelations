using PersonReplations.Domain.Entities.Abstraction;

namespace PersonReplations.Application.Interfaces;

public interface IReferenceRepository
{
  Task<IEnumerable<T>> GetReferences<T>(CancellationToken canncelationToken = default) where T : EntityBase;
  Task<bool> ValidateReference<T>(int? id, CancellationToken canncelationToken = default) where T : EntityBase;
}
