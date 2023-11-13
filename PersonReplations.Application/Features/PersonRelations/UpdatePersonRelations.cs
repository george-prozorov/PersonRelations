using FluentValidation;
using FluentValidation.Validators;
using MediatR;
using Microsoft.Extensions.Localization;
using PersonReplations.Application.Interfaces;
using PersonReplations.Application.Resources.Localization;
using PersonReplations.Domain.Entities;

namespace PersonReplations.Application.Features.PersonRelations;

public record UpdatePersonRelations(int? PersonId, IEnumerable<Relative>? Relatives) : IRequest;

public record Relative(int? RelationTypeId, int? RelativePersonId);

public class UpdatePersonRelationsHandler : IRequestHandler<UpdatePersonRelations>
{
  private readonly IUnitOfWork _unitOfWork;
  public UpdatePersonRelationsHandler(IUnitOfWork unitOfWork)
  {
    _unitOfWork = unitOfWork;
  }
  public async Task Handle(UpdatePersonRelations request, CancellationToken cancellationToken)
  {
    var relations = await _unitOfWork.PersonRepository.GetPerosnRelations(request.PersonId!.Value);

    await AddNewRelatives(relations, request);

    DeactivateRemovedRelatives(relations, request);

    await _unitOfWork.SaveChangesAsync();
  }

  private void DeactivateRemovedRelatives(List<Relation> relations, UpdatePersonRelations request)
  {
    foreach (var rel in relations)
    {
      if (!request.Relatives!
        .Where(x => x.RelationTypeId == rel.RelationTypeId &&
                    rel.PersonRelations.Any(y => y.PersonId == x.RelativePersonId))
        .Any())
      {
        rel.Deactivate();
      }
    }
  }

  private async Task AddNewRelatives(List<Relation> relations, UpdatePersonRelations request)
  {
    foreach (var relative in request.Relatives!)
    {
      if (!relations
            .Where(x =>
              x.RelationTypeId == relative.RelationTypeId &&
              x.PersonRelations.Any(x => x.PersonId == relative.RelativePersonId)
             ).Any())
      {
        await _unitOfWork.PersonRepository.AddAsync(new Relation()
        {
          RelationTypeId = relative.RelationTypeId!.Value,
          PersonRelations = new List<PersonRelation>()
           {
             new PersonRelation()
             {
               PersonId = relative.RelativePersonId!.Value
             },
             new PersonRelation()
             {
               PersonId = request.PersonId!.Value
             }
           }
        });
      }
    }
  }
}

public class UpdatePersonRelationsValidator : AbstractValidator<UpdatePersonRelations>
{
  public UpdatePersonRelationsValidator(IReferenceRepository repository, IStringLocalizer<Messages> localizer)
  {
    RuleFor(x => x.PersonId)
      .NotNull()
      .GreaterThan(0)
      .MustAsync(async (id, cancellationToken) =>
        await repository.ValidateReference<Person>(id, cancellationToken));
    RuleFor(x => x.Relatives)
      .NotNull();
    RuleFor(x => x)
      .Must(x => x.Relatives?.Any(r => r.RelativePersonId == x.PersonId) != true)
      .WithMessage(localizer["SelfRelation"]);
    RuleForEach(x => x.Relatives)
      .SetValidator(new RelativeValidator(repository));
  }
}

public class RelativeValidator : AbstractValidator<Relative>
{
  public RelativeValidator(IReferenceRepository repository)
  {
    RuleFor(x => x.RelationTypeId)
      .NotNull()
      .MustAsync(async (id, cancellationToken) =>
        await repository.ValidateReference<RelationType>(id, cancellationToken));
    RuleFor(x => x.RelativePersonId)
      .NotNull()
      .MustAsync(async (id, cancellationToken) =>
        await repository.ValidateReference<Person>(id, cancellationToken));
  }
}
