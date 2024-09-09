using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Handlers.RevokeToken;

public record RevokeTokenRequest(Guid Id) : IRequest<ResultT<Unit>>;