using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using PersonReplations.Application.Features.PersonsFeatures.Models;
using PersonReplations.Application.Interfaces;
using PersonReplations.Application.Resources.Localization;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Application.Features.PersonsFeatures;

public class GetPersonsRequest : Pagination, IRequest<GetPersonsResponse>
{
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public string? PersonalId { get; set; }

  public int? GenderId { get; set; }
  public int? CityId { get; set; }
  public int? RelativeId { get; set; }
  public DateTime? BirtDateFrom { get; set; }
  public DateTime? BirtDateTo { get; set; }
}

public class GetPersonsRequestHandler : IRequestHandler<GetPersonsRequest, GetPersonsResponse>
{
  private readonly IPersonRepository _personRepository;
  public GetPersonsRequestHandler(IPersonRepository personRepository) =>
    _personRepository = personRepository;

  public Task<GetPersonsResponse> Handle(GetPersonsRequest request, CancellationToken cancellationToken) =>
    _personRepository.GetPersons(request, cancellationToken);
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
