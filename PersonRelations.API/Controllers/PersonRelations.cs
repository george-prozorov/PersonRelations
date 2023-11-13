using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonReplations.Application.Features.PersonRelations;

namespace PersonRelations.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PersonRelations : ControllerBase
{
  private readonly ISender _sender;
  public PersonRelations(ISender sender)
  {
    _sender = sender;
  }

  [HttpPost]
  public async Task<IActionResult> Update(UpdatePersonRelations request)
  {
    await _sender.Send(request);
    return Ok();
  }
}
