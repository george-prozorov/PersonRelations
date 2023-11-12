using MediatR;
using PersonReplations.Application.Features.PersonFeatures.Models;
using PersonReplations.Application.Features.PersonsFeatures.Models;
using PersonReplations.Application.Interfaces;

namespace PersonReplations.Application.Features.PersonFeatures;

public class GetStatisticsRequest : Pagination, IRequest<GetStatisticsResponse> { }

public class GetStatisticsRequestHandler : IRequestHandler<GetStatisticsRequest, GetStatisticsResponse>
{
  private readonly IPersonRepository _repository;
  public GetStatisticsRequestHandler(IPersonRepository repository)
  {
    _repository = repository;
  }
  public Task<GetStatisticsResponse> Handle(GetStatisticsRequest request, CancellationToken cancellationToken)
  {
    //return paginatio info hear an there
    return _repository.GetStatistics(request);
  }
}
