using Identity.Domain.DTOs;
using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Handlers.Register;

public record RegisterRequest(RegisterRequestDto RegisterRequestDto): IRequest<ResultT<RegisterResponse>>;