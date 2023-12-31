﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using PersonReplations.Application.Features.PersonFeatures;

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

  [HttpGet]
  [Route("{personId}")]
  public async Task<IActionResult> Get(int personId, CancellationToken cancellationToken)
  {
    var result = await _sender.Send(new GetPersonRequest(personId), cancellationToken);
    return Ok(result);
  }

  [HttpPost]
  public async Task<IActionResult> Update(UpdatePersonRequest request)
  {
    await _sender.Send(request);
    return Ok();
  }

  [HttpPost]
  public async Task<IActionResult> Delete(DeletePersonRequest request)
  {
    await _sender.Send(request);
    return Ok();
  }
}
