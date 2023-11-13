using PersonReplations.Application.Features.PersonFeatures;
using PersonReplations.Application.Features.PersonFeatures.Models;
using PersonReplations.Application.Features.PersonsFeatures;
using PersonReplations.Application.Features.PersonsFeatures.Models;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Application.Interfaces;

public interface IPersonRepository
{
  Task AddAsync<T>(T entity) where T : class;
  Task<T?> GetByIdAsync<T>(int id) where T : class;
  Task<Person> GetPersonForUpdate(int id);
  Task<List<Relation>> GetPerosnRelations(int PersonId);
  Task<Person?> GetPersonFullInfo(int personId, CancellationToken cancellationToken);
  Task<GetPersonsResponse> GetPersons(GetPersonsRequest request, CancellationToken cancellationToken);
  Task<GetStatisticsResponse> GetStatistics(GetStatisticsRequest request, CancellationToken cancellationToken);
}
