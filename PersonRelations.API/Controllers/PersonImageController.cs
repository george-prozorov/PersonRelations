using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonReplations.Application.Features.ImageFeatures;

namespace PersonRelations.API.Controllers;

[Route("api/[controller]/[action]")]
[ApiController]
public class PersonImageController : ControllerBase
{
  private readonly ISender _sender;
  public PersonImageController(ISender sender)
  {
    _sender = sender;
  }

  [HttpPost]
  public async Task<IActionResult> Set([FromForm] SetImageRequest request)
  {
    await _sender.Send(request);
    return Ok();
  }
}
