using Identity.Application.Handlers.Login;
using Identity.Application.Handlers.Logout;
using Identity.Domain.DTOs;
using Identity.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using LoginRequest = Identity.Application.Handlers.Login.LoginRequest;
using RegisterRequest = Identity.Application.Handlers.Register.RegisterRequest;

namespace Identity.WebAPI.Controllers;

[ApiController]
[Route("api/identity")]
public class IdentityController(ISender sender) : ControllerBase
{
    [HttpPost(nameof(Login))]
    public async Task<ActionResult<ResultT<LoginResponse>>> Login([FromBody] LoginRequestDto loginRequestDto)
    {
        var result = await sender.Send(new LoginRequest(loginRequestDto));
        return result.IsSuccess ? Ok(result) : BadRequest($"{result.Error.Code}: {result.Error.Message}");
    }
    
    [HttpPost(nameof(Register))]
    public async Task<ActionResult<ResultT<Guid>>> Register([FromBody] RegisterRequestDto registerRequestDto)
    {
        var result = await sender.Send(new RegisterRequest(registerRequestDto));
        return result.IsSuccess ? Ok(result) : BadRequest($"{result.Error.Code}: {result.Error.Message}");
    }

    [HttpDelete($"{nameof(Logout)}/" + "{id}")]
    public async Task<ActionResult<ResultT<Unit>>> Logout(Guid id)
    {
        var result = await sender.Send(new LogoutRequest(id));
        return result.IsSuccess ? Ok(result) : BadRequest($"{result.Error.Code}: {result.Error.Message}");
    }
}