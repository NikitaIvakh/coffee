using Coffee.Application.Coffees.Handlers.Create;
using Coffee.Application.Coffees.Handlers.Delete;
using Coffee.Application.Coffees.Handlers.Update;
using Coffee.Application.Coffees.Queries.CoffeeById;
using Coffee.Application.Coffees.Queries.CoffeeList;
using Coffee.Domain.DTOs;
using Coffee.Domain.Shared;
using Coffee.WebAPI.Abstractors;
using Coffee.WebAPI.Utils;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using static Coffee.WebAPI.Utils.StaticDetails;

namespace Coffee.WebAPI.Controllers;

[Authorize]
[ApiController]
[Route("api/coffee")]
public class CoffeeController(ISender sender) : ApiController(sender)
{
    private readonly ISender _sender = sender;

    [HttpGet(nameof(GetCoffeeList))]
    [Authorize(Roles = AdministratorOrUser)]
    public async Task<ActionResult<ResultT<PaginationList<GetCoffeeListDto>>>> GetCoffeeList(string? search,
        string? filter, int page, int pageSize, int? limit = null)
    {
        var query = await _sender.Send(new GetCoffeeQuery(search, filter, page, pageSize, limit));
        return query.IsSuccess ? Ok(query) : HandleFailure<PaginationList<GetCoffeeListDto>>(query);
    }

    [HttpGet(nameof(GetCoffee) + "/{id}")]
    [Authorize(Roles = AdministratorOrUser)]
    public async Task<ActionResult<ResultT<GetCoffeeDto>>> GetCoffee(Guid id)
    {
        var query = await _sender.Send(new GetCoffeeByIdQuery(id));
        return query.IsSuccess ? Ok(query) : HandleFailure<GetCoffeeDto>(query);
    }

    [HttpPost(nameof(CreateCoffee))]
    [Authorize(Roles = RoleAdministrator)]
    public async Task<ActionResult<ResultT<Guid>>> CreateCoffee([FromForm] CreateCoffeeDto createCoffeeDto)
    {
        var command = await _sender.Send(new CreateCoffeeCommand(createCoffeeDto));
        return command.IsSuccess ? CreatedAtAction(nameof(CreateCoffee), new { Id = command.Value }, command.Value) : HandleFailure<Guid>(command);
    }

    [HttpPatch($"{nameof(UpdateCoffee)}/" + "{id}")]
    [Authorize(Roles = RoleAdministrator)]
    public async Task<ActionResult<ResultT<Unit>>> UpdateCoffee(Guid id, [FromForm] UpdateCoffeeDto updateCoffeeDto)
    {
        var command = await _sender.Send(new UpdateCoffeeCommand(updateCoffeeDto));
        return command.IsSuccess ? Ok(command) : HandleFailure<Unit>(command);
    }

    [HttpDelete($"{nameof(DeleteCoffee)}/" + "{id}")]
    [Authorize(Roles = RoleAdministrator)]
    public async Task<ActionResult<ResultT<Unit>>> DeleteCoffee(Guid id)
    {
        var command = await _sender.Send(new DeleteCoffeeCommand(id));
        return command.IsSuccess ? Ok(command) : HandleFailure<Unit>(command);
    }
}