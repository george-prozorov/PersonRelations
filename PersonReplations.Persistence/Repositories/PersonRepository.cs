using Microsoft.EntityFrameworkCore;
using PersonReplations.Application.Interfaces;

namespace PersonReplations.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
  private readonly PersonRelationsDbContext _db;
  public PersonRepository(PersonRelationsDbContext db)
  {
    _db = db;
  }

  public async Task AddAsync<T>(T entity) where T : class
  {
    await _db.Set<T>().AddAsync(entity);
  }

  public async Task<T?> GetByIdAsync<T>(int id) where T : class
  {
    return await _db.Set<T>().FindAsync(id);
  }
}
