using PersonReplations.Application.Features.PersonFeatures;
using PersonReplations.Application.Features.PersonFeatures.Models;
using PersonReplations.Application.Features.PersonsFeatures;
using PersonReplations.Application.Features.PersonsFeatures.Models;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Application.Interfaces;

public interface IPersonRepository
{
  Task AddAsync<T>(T entity, CancellationToken cancellationToken = default) where T : class;
  Task<T?> GetByIdAsync<T>(int id, CancellationToken cancellationToken = default) where T : class;
  Task<Person> GetPersonForUpdate(int id, CancellationToken cancellationToken = default);
  Task<List<Relation>> GetPerosnRelations(int PersonId, CancellationToken cancellationToken = default);
  Task<Person?> GetPersonFullInfo(int personId, CancellationToken cancellationToken);
  Task<GetPersonsResponse> GetPersons(GetPersonsRequest request, CancellationToken cancellationToken);
  Task<GetStatisticsResponse> GetStatistics(GetStatisticsRequest request, CancellationToken cancellationToken);
}
