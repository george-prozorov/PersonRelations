using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonReplations.Application.Features.PersonFeatures;
using PersonReplations.Application.Features.PersonsFeatures;

namespace PersonRelations.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PersonsController : ControllerBase
{
  private readonly ISender _sender;
  public PersonsController(ISender sender)
  {
    _sender = sender;
  }
  [HttpPost]
  public async Task<IActionResult> GetPersons(GetPersonsRequest request, CancellationToken cancellationToken)
  {
    var result = await _sender.Send(request, cancellationToken);
    return Ok(result);
  }
  [HttpPost]
  public async Task<IActionResult> GetStatistiics(GetStatisticsRequest request, CancellationToken cancellationToken)
  {
    var resul = await _sender.Send(request, cancellationToken);
    return Ok(resul);
  }
}
