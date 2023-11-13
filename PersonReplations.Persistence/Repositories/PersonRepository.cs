using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PersonReplations.Application.Features.PersonFeatures;
using PersonReplations.Application.Features.PersonFeatures.Models;
using PersonReplations.Application.Features.PersonsFeatures;
using PersonReplations.Application.Features.PersonsFeatures.Models;
using PersonReplations.Application.Interfaces;
using PersonReplations.Domain.Entities;
using PersonReplations.Persistence.Helpers;

namespace PersonReplations.Persistence.Repositories;

public class PersonRepository : IPersonRepository
{
  private readonly PersonRelationsDbContext _db;
  private readonly IMapper _mapper;
  public PersonRepository(PersonRelationsDbContext db, IMapper mapper)
  {
    _db = db;
    _mapper = mapper;
  }

  public async Task AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class
  {
    await _db.Set<T>().AddAsync(entity, cancellationToken);
  }

  public async Task<T?> GetByIdAsync<T>(int id, CancellationToken cancellationToken = default) where T : class
  {
    return await _db.Set<T>().FindAsync(id, cancellationToken);
  }

  public async Task<List<Relation>> GetPerosnRelations(int PersonId, CancellationToken cancellationToken = default)
  {
    return await _db.Relations
      .Include(x => x.PersonRelations)
      .Where(x => x.PersonRelations.Any(x => x.PersonId == PersonId))
      .ToListAsync(cancellationToken);
  }

  public Task<Person> GetPersonForUpdate(int id, CancellationToken cancellationToken = default)
  {
    return _db.Persons
      .Include(x => x.Contacts)
      .Include(x => x.PersonRelations)
        .ThenInclude(x => x.Relation)
          .ThenInclude(x => x!.PersonRelations)
      .FirstAsync(x => x.Id == id, cancellationToken);
  }

  public Task<Person?> GetPersonFullInfo(int personId, CancellationToken cancellationToken)
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
    return query.FirstOrDefaultAsync(x => x.Id == personId, cancellationToken);
  }

  public async Task<GetPersonsResponse> GetPersons(GetPersonsRequest request, CancellationToken cancellationToken)
  {
    var result = new GetPersonsResponse();

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
        (!request.BirtDateFrom.HasValue || x.BirthDate >= request.BirtDateFrom.Value.Date) &&
        (!request.BirtDateTo.HasValue || x.BirthDate <= request.BirtDateTo.Value.Date) &&
        (!request.RelativeId.HasValue ||
        x.PersonRelations.Any(y => y.Relation!.PersonRelations
            .Any(z => z.PersonId != x.Id && z.PersonId == request.RelativeId)))
      ).Select(x => _mapper.Map<PersonsListItem>(x));
    result.CurrentPage = request.PageNumber!.Value;
    result.TotalRecords = await query.CountAsync(cancellationToken);
    result.Persons = await query
                            .Paginate(request.PageNumber!.Value, request.PageSize!.Value)
                            .ToListAsync(cancellationToken);
    return result;
  }

  public async Task<GetStatisticsResponse> GetStatistics(GetStatisticsRequest request, CancellationToken cancellationToken)
  {
    var result = new GetStatisticsResponse();
    var query = _db.Persons
      .Join(_db.PersonRelations
                  .Include(x => x.Relation)
                    .ThenInclude(x => x!.RelationType),
                  p => p.Id,
                  pr => pr.PersonId,
                  (p, pr) => new { Person = p, PersonRelation = pr })
      .GroupBy(x => new
      {
        x.Person.FirstName,
        x.Person.LastName,
        RelationType = x.PersonRelation.Relation!.RelationType!.DisplayName
      })
      .Select(x => new StatisticsListItem
      {
        Person = x.Key.FirstName + " " + x.Key.LastName,
        RelationType = x.Key.RelationType,
        Count = x.Count(),
      });
    result.Stats = await query
                          .Paginate(request.PageNumber!.Value, request.PageSize!.Value)
                          .ToListAsync(cancellationToken);
    result.CurrentPage = request.PageNumber!.Value;
    result.TotalRecords = await query.CountAsync(cancellationToken);
    return result;
  }
}
