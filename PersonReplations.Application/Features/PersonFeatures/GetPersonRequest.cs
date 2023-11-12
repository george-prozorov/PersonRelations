using MediatR;
using PersonReplations.Application.Features.PersonFeatures.Models;

namespace PersonReplations.Application.Features.PersonFeatures;

public record GetPersonRequest(int PersonId) : IRequest<GetPersonResponse>;

public class GetPersonRequestHandler : IRequestHandler<GetPersonRequest, GetPersonResponse>
{
  public Task<GetPersonResponse> Handle(GetPersonRequest request, CancellationToken cancellationToken)
  {
    throw new NotImplementedException();
  }
}
