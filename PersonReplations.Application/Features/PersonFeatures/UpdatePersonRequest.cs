using FluentValidation;
using MediatR;

namespace PersonReplations.Application.Features.PersonFeatures;

public class UpdatePersonRequest : AddPersonRequest, IRequest
{
  public int? PersonId { get; set; }
}

internal class UpdatePersonRequestHandler : IRequestHandler<UpdatePersonRequest>
{
  public Task Handle(UpdatePersonRequest request, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}

internal class UpdatePersonRequestValidator : AbstractValidator<UpdatePersonRequest>
{
    
}
