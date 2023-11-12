using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using PersonReplations.Application.Interfaces;
using PersonReplations.Application.Resources.Localization;
using PersonReplations.Domain.Entities;
using PersonReplations.Application.Helpers;

namespace PersonReplations.Application.Features.PersonFeatures;

public class UpdatePersonRequest : AddPersonRequest, IRequest
{
  public int? PersonId { get; set; }
}

internal class UpdatePersonRequestHandler : IRequestHandler<UpdatePersonRequest>
{
  private readonly IUnitOfWork _unitOfWork;
  private readonly IMapper _mapper;
  public UpdatePersonRequestHandler(IUnitOfWork unitOfWork, IMapper mapper)
  {
    _unitOfWork = unitOfWork;
    _mapper = mapper;
  }
  public async Task Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
  {
    var person = await _unitOfWork.PersonRepository.GetPersonForUpdate((int)request.PersonId!);
    person!.Update(request.FirstName,
                   request.LastName,
                   request.Genderid,
                   request.PersonalId,
                   request.BirthDate,
                   request.CityId);

    person.UpdateContacts(_mapper.Map<IEnumerable<Contact>>(request.Contacts));

    await _unitOfWork.SaveChangesAsync();
  }
}

public class UpdatePersonRequestValidator : AbstractValidator<UpdatePersonRequest>
{
  public UpdatePersonRequestValidator(IStringLocalizer<Messages> localizer, IReferenceRepository repository)
  {
    RuleFor(x => x.PersonId)
      .NotNull()
      .GreaterThan(0)
      .MustAsync(async (id, cancelationToken) =>
          await repository.ValidateReference<Person>(id, cancelationToken));
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
        await repository.ValidateReference<Gender>(id, canncelationToken)
      );
    RuleFor(x => x.BirthDate)
      .NotNull()
      .Must(x => x.IsMature())
      .WithMessage(x => string.Format(localizer["NotMature"], nameof(x.BirthDate)));
    RuleFor(x => x.PersonalId)
      .NotNull()
      .Length(11)
      .Matches(@"^\d{11}$");
    RuleFor(x => x.CityId)
      .NotNull()
      .GreaterThan(0)
      .MustAsync(async (id, canncelationToken) =>
        await repository.ValidateReference<City>(id, canncelationToken)
      );
    RuleFor(x => x.Contacts)
      .NotNull();

    RuleForEach(x => x.Contacts)
      .SetValidator(new ContactValidator(repository));
  }
}
