using Microsoft.EntityFrameworkCore;
using PersonReplations.Application.Features.PersonsFeatures;
using PersonReplations.Application.Interfaces;
using PersonReplations.Domain.Entities;

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

  public async Task<List<Relation>> GetPerosnRelations(int PersonId)
  {
    return await _db.Relations
      .Include(x => x.PersonRelations)
      .Where(x => x.PersonRelations.Any(x => x.PersonId == PersonId))
      .ToListAsync();
  }

  public Task<Person> GetPersonForUpdate(int id)
  {
    return _db.Persons
      .Include(x => x.Contacts)
      .Include(x => x.PersonRelations)
      .ThenInclude(x => x.Relation)
      .ThenInclude(x => x!.PersonRelations)
      .FirstAsync(x => x.Id == id);
  }

  public Task<Person?> GetPersonFullInfo(int personId)
  {
    var query = _db.Persons
      .Include(x => x.Gender)
      .Include(x => x.City)
      .Include(x => x.Contacts)
      .ThenInclude(x => x.ContactType)
      .Include(x => x.PersonRelations)
      .ThenInclude(x => x.Relation)
      .ThenInclude(x => x!.PersonRelations)
      .ThenInclude(x => x.Person)
      .Include(x => x.PersonRelations)
      .ThenInclude(x => x.Relation)
      .ThenInclude(x => x!.RelationType);
    var cmd = query.ToQueryString();
    return query.FirstOrDefaultAsync(x => x.Id == personId);
  }

  public async Task<IEnumerable<Person>> GetPersons(GetPersonsRequest request)
  {
    var query = _db.Persons
      .Include(x => x.Gender)
      .Include(x => x.City)
      .Include(x => x.PersonRelations)
      .ThenInclude(x => x.Relation)
      .ThenInclude(x => x!.PersonRelations)
      .Where(x =>
        (string.IsNullOrEmpty(request.FirstName) || EF.Functions.Like(x.FirstName, "%" + request.FirstName + "%")) &&
        (string.IsNullOrEmpty(request.LastName) || EF.Functions.Like(x.LastName, "%" + request.LastName + "%")) &&
        (string.IsNullOrEmpty(request.PersonalId) || EF.Functions.Like(x.PersonalId, "%" + request.PersonalId + "%")) &&
        (!request.GenderId.HasValue || x.GenderId == request.GenderId) &&
        (!request.CityId.HasValue || x.CityId == request.CityId) &&
        (!request.RelativeId.HasValue ||
        x.PersonRelations.Any(y => y.Relation!.PersonRelations
            .Any(z => z.PersonId != x.Id && z.PersonId == request.RelativeId)))
      );
    return await query.ToListAsync();
  }
}
