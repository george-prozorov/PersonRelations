using FluentValidation;
using MediatR;

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

  public class Contact
  {
    public int? ContactTypeId { get; set; }
    public string? Value { get; set; }
  }
}

public class AddPersonRequestHandler : IRequestHandler<AddPersonRequest>
{
  public Task Handle(AddPersonRequest request, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}

public class AddPersonRequestValidator : AbstractValidator<AddPersonRequest>
{
  public AddPersonRequestValidator()
  {
    RuleFor(x => x.FirstName)
      .NotNull()
      .Matches("^(?:[A-Za-z]{2,50}|[ა-ჰ]{2,50})$");
    RuleFor(x => x.LastName)
      .NotNull()
      .Matches("^(?:[A-Za-z]{2,50}|[ა-ჰ]{2,50})$");
  }
}
