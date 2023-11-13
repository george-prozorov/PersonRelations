using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using PersonReplations.Application.Interfaces;
using PersonReplations.Domain.Entities.Abstraction;

namespace PersonReplations.Persistence.Repositories;

public class ReferenceRepository : IReferenceRepository
{
  private readonly PersonRelationsDbContext _db;
  private readonly IMemoryCache _memoryCache;

  public ReferenceRepository(PersonRelationsDbContext db, IMemoryCache memoryCache)
  {
    _db = db;
    _memoryCache = memoryCache;
  }
  public async Task<IEnumerable<T>> GetReferences<T>(CancellationToken canncelationToken = default) where T : EntityBase
  {
    var memResult = _memoryCache.Get<IEnumerable<T>>(typeof(T).Name);
    if (memResult != null)
      return memResult;
    var result = await _db.Set<T>().ToListAsync(canncelationToken);
    _memoryCache.Set(typeof(T).Name, result, TimeSpan.FromHours(3));
    return result;
  }
  public async Task<bool> ValidateReference<T>(int? id, CancellationToken canncelationToken = default) where T : EntityBase
  {
    if (!id.HasValue) return false;
    if (typeof(T).Name == "Person")
    {
      return await _db.Persons.AnyAsync(x => x.Id == id, canncelationToken);
    }
    var refs = await GetReferences<T>(canncelationToken);
    return refs.Any(x => x.Id == id);
  }
}
