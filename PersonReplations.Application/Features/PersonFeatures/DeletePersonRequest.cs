using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using PersonReplations.Application.Interfaces;
using PersonReplations.Application.Resources.Localization;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Application.Features.PersonFeatures;

public record DeletePersonRequest(int? PersonId) : IRequest;

public class DeletePersonRequestHandler : IRequestHandler<DeletePersonRequest>
{
  private readonly IUnitOfWork _unitOfWork;
  public DeletePersonRequestHandler(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }
  public async Task Handle(DeletePersonRequest request, CancellationToken cancellationToken)
  {
    var person = await _unitOfWork.PersonRepository.GetPersonForUpdate(request.PersonId!.Value);
    person.Deactivate();
    await _unitOfWork.SaveChangesAsync();
  }
}

public class DeletePersonRequestValidator : AbstractValidator<DeletePersonRequest>
{
  public DeletePersonRequestValidator(IReferenceRepository repository, IStringLocalizer<Messages> localizer)
  {
    RuleFor(x => x.PersonId)
      .NotNull()
      .GreaterThan(0)
      .MustAsync(async (id, cancelationToken) =>
        await repository.ValidateReference<Person>(id, cancelationToken))
      .WithMessage(localizer["NotFound"]);
  }
}
