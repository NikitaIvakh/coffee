using Identity.Domain.DTOs;
using Identity.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.Application.Handlers.RefreshToken;

public record RefreshTokenRequest(TokenModelDto TokenModelDto): IRequest<ResultT<ObjectResult>>;