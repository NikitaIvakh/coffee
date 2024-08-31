using Coffee.Application.Coffees.Handlers.Create;
using Coffee.Application.Coffees.Handlers.Delete;
using Coffee.Application.Coffees.Handlers.Update;
using Coffee.Application.Coffees.Queries.CoffeeById;
using Coffee.Application.Coffees.Queries.CoffeeList;
using Coffee.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.WebAPI.Controllers;

[ApiController]
[Route("api/coffee")]
public class CoffeeController(IMediator mediator) : ControllerBase
{
    [HttpGet(nameof(GetCoffeeList))]
    public async Task<ActionResult<GetCoffeeListDto>> GetCoffeeList()
    {
        var query = await mediator.Send(new GetCoffeeQuery());
        return Ok(query);
    }

    [HttpGet(nameof(GetCoffee) + "{id}")]
    public async Task<ActionResult<GetCoffeeDto>> GetCoffee(Guid id)
    {
        var query = await mediator.Send(new GetCoffeeByIdQuery(id));
        return Ok(query);
    }
    
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

    [HttpDelete($"{nameof(DeleteCoffee)}/" + "{id}")]
    public async Task<ActionResult<Unit>> DeleteCoffee(Guid id, [FromBody] DeleteCoffeeDto deleteCoffeeDto)
    {
        var command = await mediator.Send(new DeleteCoffeeCommand(deleteCoffeeDto));
        return Ok(command);
    }
}