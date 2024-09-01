using Coffee.Application.Coffees.Handlers.Create;
using Coffee.Application.Coffees.Handlers.Delete;
using Coffee.Application.Coffees.Handlers.Update;
using Coffee.Application.Coffees.Handlers.UploadPhoto;
using Coffee.Application.Coffees.Queries.CoffeeById;
using Coffee.Application.Coffees.Queries.CoffeeList;
using Coffee.Domain.DTOs;
using Coffee.Domain.Shared;
using Coffee.WebAPI.Abstractors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Coffee.WebAPI.Controllers;

[ApiController]
[Route("api/coffee")]
public class CoffeeController(ISender sender) : ApiController(sender)
{
    private readonly ISender _sender = sender;

    [HttpGet(nameof(GetCoffeeList))]
    public async Task<ActionResult<ResultT<List<GetCoffeeListDto>>>> GetCoffeeList()
    {
        var query = await _sender.Send(new GetCoffeeQuery());
        return query.IsSuccess ? Ok(query) : HandleFailure<List<GetCoffeeListDto>>(query);
    }

    [HttpGet(nameof(GetCoffee) + "/{id}")]
    public async Task<ActionResult<ResultT<GetCoffeeDto>>> GetCoffee(Guid id)
    {
        var query = await _sender.Send(new GetCoffeeByIdQuery(id));
        return query.IsSuccess ? Ok(query) : HandleFailure<GetCoffeeDto>(query);
    }
    
    [HttpPost(nameof(CreateCoffee))]
    public async Task<ActionResult<ResultT<Guid>>> CreateCoffee([FromBody] CreateCoffeeDto createCoffeeDto)
    {
        var command = await _sender.Send(new CreateCoffeeCommand(createCoffeeDto));
        return command.IsSuccess ? CreatedAtAction(nameof(CreateCoffee), new {Id = command.Value}, command.Value) : HandleFailure<Guid>(command);
    }

    [HttpPost(nameof(UploadPhoto))]
    public async Task<ActionResult<ResultT<string>>> UploadPhoto([FromServices] UploadCoffeePhotoHandler handler, [FromForm] UploadCoffeePhotoRequest request, CancellationToken token)
    {
        var result = await handler.Handle(request, token);
        return result.IsSuccess ? Ok(result) : HandleFailure<string>(result);
    }
    
    [HttpPatch($"{nameof(UpdateCoffee)}/" + "{id}")]
    public async Task<ActionResult<ResultT<Unit>>> UpdateCoffee(Guid id, [FromBody] UpdateCoffeeDto updateCoffeeDto)
    {
        var command = await _sender.Send(new UpdateCoffeeCommand(updateCoffeeDto));
        return command.IsSuccess ? Ok(command) : HandleFailure<Unit>(command);
    }

    [HttpDelete($"{nameof(DeleteCoffee)}/" + "{id}")]
    public async Task<ActionResult<ResultT<Unit>>> DeleteCoffee(Guid id, [FromBody] DeleteCoffeeDto deleteCoffeeDto)
    {
        var command = await _sender.Send(new DeleteCoffeeCommand(deleteCoffeeDto));
        return command.IsSuccess ? Ok(command) : HandleFailure<Unit>(command);
    }
}