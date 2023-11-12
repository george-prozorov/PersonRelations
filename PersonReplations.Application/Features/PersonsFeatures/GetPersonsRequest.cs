using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using PersonReplations.Application.Features.PersonsFeatures.Models;
using PersonReplations.Application.Interfaces;
using PersonReplations.Application.Resources.Localization;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Application.Features.PersonsFeatures;

public class GetPersonsRequest : Pagination, IRequest<IEnumerable<GetPersonsResponse>>
{
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? PersonalId { get; set; }

  public int? GenderId { get; set; }
  public int? CityId { get; set; }
  public int? RelativeId { get; set; }
}

public class GetPersonsRequestHandler : IRequestHandler<GetPersonsRequest, IEnumerable<GetPersonsResponse>>
{
  private readonly IPersonRepository _personRepository;
  private readonly IMapper _mapper;
  public GetPersonsRequestHandler(IPersonRepository personRepository, IMapper mapper)
  {
    _personRepository = personRepository;
    _mapper = mapper;
  }
  public async Task<IEnumerable<GetPersonsResponse>> Handle(GetPersonsRequest request, CancellationToken cancellationToken)
  {
    IEnumerable<Person> persons = await _personRepository.GetPersons(request);
    return _mapper.Map<IEnumerable<GetPersonsResponse>>(persons);
  }
}

public class GetPersonsRequestValidator : AbstractValidator<GetPersonsRequest>
{
  public GetPersonsRequestValidator(IStringLocalizer<Messages> localizer, IReferenceRepository repository)
  {
    RuleFor(x => x.FirstName)
      .MaximumLength(50)
      .Matches(@"^[A-Za-zა-ჰ]+$")
      .WithMessage(x => string.Format(localizer["NotAllowedCharacters"], nameof(x.FirstName)))
      .Matches(@"^(?:[A-Za-z]+|[ა-ჰ]+)$")
      .WithMessage(x => string.Format(localizer["NotTogether"], nameof(x.FirstName)));
    RuleFor(x => x.LastName)
      .MaximumLength(50)
      .Matches(@"^[A-Za-zა-ჰ]+$")
      .WithMessage(x => string.Format(localizer["NotAllowedCharacters"], nameof(x.LastName)))
      .Matches(@"^(?:[A-Za-z]+|[ა-ჰ]+)$")
      .WithMessage(x => string.Format(localizer["NotTogether"], nameof(x.LastName)));
    RuleFor(x => x.PersonalId)
      .Length(11)
      .Matches(@"^\d{11}$");
    RuleFor(x => x.GenderId)
      .GreaterThan(0)
      .MustAsync(async (id, canncelationToken) =>
      {
        if (!id.HasValue) return true;
        return await repository.ValidateReference<Gender>(id, canncelationToken);
      });
    RuleFor(x => x.CityId)
      .GreaterThan(0)
      .MustAsync(async (id, canncelationToken) =>
      {
        if (!id.HasValue) return true;
        return await repository.ValidateReference<City>(id, canncelationToken);
      });
    RuleFor(x => x.RelativeId)
      .GreaterThan(0)
      .MustAsync(async (id, canncelationToken) =>
      {
        if (!id.HasValue) return true;
        return await repository.ValidateReference<Person>(id, canncelationToken);
      });
    RuleFor(x => x.PageNumber)
      .NotNull()
      .GreaterThan(0);
    RuleFor(x => x.PageSize)
      .NotNull()
      .GreaterThan(0);
  }
}
