using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using PersonReplations.Application.Interfaces;
using PersonReplations.Application.Resources.Localization;
using PersonReplations.Domain.Entities;
using PersonReplations.Domain.Entities.Abstraction;

namespace PersonReplations.Application.Features.PersonFeatures;

public class AddPersonRequest : IRequest
{
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public int? Genderid { get; set; }
  public string? PersonalId { get; set; }
  public DateTime? Birthday { get; set; }
  public int? CityId { get; set; }

  public IEnumerable<AddContactRequest>? Contacts { get; set; }
}

public class AddContactRequest
{
  public int? ContactTypeId { get; set; }
  public string? Value { get; set; }
}

public class AddPersonRequestHandler : IRequestHandler<AddPersonRequest>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  public AddPersonRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }
  public async Task Handle(AddPersonRequest request, CancellationToken cancellationToken)
  {
    //BirthDate Validation
    //latinuri da qartuli asoebis narevi aq xom ar gamovitano?
    //piradi nomris lenght xom ar gamoitano aq?
    //check cities
    //check contact types
    //check GenderId
    var person = _mapper.Map<Person>(request);
    await _unitOfWork.PersonRepository.AddAsync(person);
    await _unitOfWork.SaveChangesAsync();
  }
}

public class AddPersonRequestValidator : AbstractValidator<AddPersonRequest>
{
  public AddPersonRequestValidator(IStringLocalizer<Messages> localizer, IReferenceRepository repository)
  {

    RuleFor(x => x.FirstName)
      .NotNull()
      .MinimumLength(2)
      .MaximumLength(50)
      .Matches(@"^[A-Za-zა-ჰ]+$")
      .WithMessage(x => string.Format(localizer["NotAllowedCharacters"], nameof(x.FirstName)))
      .Matches(@"^(?:[A-Za-z]+|[ა-ჰ]+)$")
      .WithMessage(x => string.Format(localizer["NotTogether"], nameof(x.FirstName)));
    RuleFor(x => x.LastName)
      .NotNull()
      .MinimumLength(2)
      .MaximumLength(50)
      .Matches(@"^[A-Za-zა-ჰ]+$")
      .WithMessage(x => string.Format(localizer["NotAllowedCharacters"], nameof(x.LastName)))
      .Matches(@"^(?:[A-Za-z]+|[ა-ჰ]+)$")
      .WithMessage(x => string.Format(localizer["NotTogether"], nameof(x.LastName)));
    RuleFor(x => x.Genderid)
      .NotNull()
      .GreaterThan(0)
      .MustAsync(async (id, canncelationToken) =>
        await repository.ValidateReference<Gender>((int)id!, canncelationToken)
      );

    RuleFor(x => x.PersonalId)
      .NotNull()
      .Length(11)
      .Matches(@"^\d{11}$");
    RuleFor(x => x.Birthday)
      .NotNull();
    RuleFor(x => x.CityId)
      .NotNull()
      .GreaterThan(0)
      .MustAsync(async (id, canncelationToken) =>
        await repository.ValidateReference<City>((int)id!, canncelationToken)
      );
    RuleFor(x => x.Contacts)
      .NotNull();

    RuleForEach(x => x.Contacts)
      .SetValidator(new ContactValidator(repository));
  }
}

public class ContactValidator : AbstractValidator<AddContactRequest>
{
  public ContactValidator(IReferenceRepository repository)
  {
    RuleFor(x => x.ContactTypeId)
     .NotNull()
     .GreaterThan(0)
     .MustAsync(async (id, canncelationToken) =>
      await repository.ValidateReference<ContactType>((int)id!, canncelationToken)
    ); ;
    RuleFor(x => x.Value)
      .NotNull()
      .MinimumLength(4)
      .MaximumLength(50);
  }
}
