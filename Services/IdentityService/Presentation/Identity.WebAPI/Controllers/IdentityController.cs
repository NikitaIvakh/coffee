using Identity.Domain.DTOs;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using RegisterRequest = Identity.Application.Handlers.Register.RegisterRequest;

namespace Identity.WebAPI.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(ISender sender) : ControllerBase
{
    [HttpPost(nameof(Register))]
    public async Task<ActionResult<Guid>> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        var result = await sender.Send(new RegisterRequest(registerRequestDto));
        return result.IsSuccess ? Ok(result) : BadRequest($"{result.Error.Code}: {result.Error.Message}");
    }
}