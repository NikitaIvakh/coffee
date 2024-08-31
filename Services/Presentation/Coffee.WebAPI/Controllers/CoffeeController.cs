using Coffee.Application.Coffees.Handlers.Create;
using Coffee.Application.Coffees.Handlers.Update;
using Coffee.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.WebAPI.Controllers;

[ApiController]
[Route("api/coffee")]
public class CoffeeController(IMediator mediator) : ControllerBase
{
    [HttpPost(nameof(CreateCoffee))]
    public async Task<ActionResult> CreateCoffee([FromBody] CreateCoffeeDto createCoffeeDto)
    {
        var command = await mediator.Send(new CreateCoffeeCommand(createCoffeeDto));
        return Ok(command);
    }

    [HttpPatch($"{nameof(UpdateCoffee)}/" + "{id}")]
    public async Task<ActionResult<Unit>> UpdateCoffee(Guid id, [FromBody] UpdateCoffeeDto updateCoffeeDto)
    {
        var command = await mediator.Send(new UpdateCoffeeCommand(updateCoffeeDto));
        return Ok(command);
    }
}