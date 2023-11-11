using FluentValidation;
using MediatR;
using PersonReplations.Application.Interfaces;

namespace PersonReplations.Application.Features.Person;

public class AddPersonRequest : IRequest
{
  public string? FirstName { get; set; }
  public string? LastName { get; set; }
  public int? Gender { get; set; }
  public string? PersonalId { get; set; }
  public DateTime? Birthday { get; set; }
  public int? CityId { get; set; }

  public IEnumerable<Contact>? Contacts { get; set; }
}

public class Contact
{
  public int? ContactTypeId { get; set; }
  public string? Value { get; set; }
}

public class AddPersonRequestHandler : IRequestHandler<AddPersonRequest>
{
  private readonly IUnitOfWork _unitOfWork;
  public AddPersonRequestHandler(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }
  public Task Handle(AddPersonRequest request, CancellationToken cancellationToken)
  {
    //BirthDate Validation
    //latinuri da qartuli asoebis narevi aq xom ar gamovitano?
    //piradi nomris lenght xom ar gamoitano aq?
    throw new NotImplementedException();
  }
}

public class AddPersonRequestValidator : AbstractValidator<AddPersonRequest>
{
  public AddPersonRequestValidator()
  {
    RuleFor(x => x.FirstName)
      .NotNull()
      .Matches(@"^(?:[A-Za-z]{2,50}|[ა-ჰ]{2,50})$");
    RuleFor(x => x.LastName)
      .NotNull()
      .Matches(@"^(?:[A-Za-z]{2,50}|[ა-ჰ]{2,50})$");
    RuleFor(x => x.Gender)
      .NotNull()
      .GreaterThan(0);
    RuleFor(x => x.PersonalId)
      .NotNull()
      .Length(11)
      .Matches(@"^\d{11}$");
    RuleFor(x => x.Birthday)
      .NotNull();
    RuleFor(x => x.CityId)
      .NotNull()
      .GreaterThan(0);
    RuleFor(x => x.Contacts)
      .NotNull();

    RuleForEach(x => x.Contacts)
      .SetValidator(new ContactValidator());
  }
}

public class ContactValidator : AbstractValidator<Contact>
{
  public ContactValidator()
  {
    RuleFor(x => x.ContactTypeId)
     .NotNull()
     .GreaterThan(0);
    RuleFor(x => x.Value)
      .NotNull()
      .MinimumLength(4)
      .MaximumLength(50);
  }
}
