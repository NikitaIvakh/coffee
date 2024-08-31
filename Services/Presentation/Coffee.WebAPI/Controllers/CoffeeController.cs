using Coffee.Application.Coffees.Handlers.Create;
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
}