using FluentValidation;
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

  public Task<GetStatisticsResponse> Handle(GetStatisticsRequest request, CancellationToken cancellationToken) =>
     _repository.GetStatistics(request, cancellationToken);
}

public class GetStatisticsRequestValidator : AbstractValidator<GetStatisticsRequest>
{
  public GetStatisticsRequestValidator()
  {
    RuleFor(x => x.PageNumber)
      .NotNull()
      .GreaterThan(0);
    RuleFor(x => x.PageSize)
      .NotNull()
      .GreaterThan(0);
  }
}
