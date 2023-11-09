using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonReplations.Application.Features.Person;

namespace PersonRelations.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PersonController : ControllerBase
{
  private readonly ISender _sender;
  public PersonController(ISender sender)
  {
    _sender = sender;
  }

  [HttpPost]
  public async Task<IActionResult> Add(AddPersonRequest request)
  {
    await _sender.Send(request);
    return Ok();
  }
}
