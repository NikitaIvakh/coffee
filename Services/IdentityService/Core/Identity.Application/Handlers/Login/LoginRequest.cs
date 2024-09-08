using Identity.Domain.DTOs;
using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Handlers.Login;

public record LoginRequest(LoginRequestDto LoginRequestDto) : IRequest<ResultT<LoginResponse>>;