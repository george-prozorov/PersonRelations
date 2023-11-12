using PersonReplations.Domain.Entities.Abstraction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersonReplations.Application.Interfaces
{
  public interface IReferenceRepository
  {
    Task<IEnumerable<T>> GetReferences<T>(CancellationToken canncelationToken) where T : EntityBase;
    Task<bool> ValidateReference<T>(int id, CancellationToken canncelationToken) where T : EntityBase;
  }
}
