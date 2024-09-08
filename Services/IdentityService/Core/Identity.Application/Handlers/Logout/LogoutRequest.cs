using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Handlers.Logout;

public record LogoutRequest(Guid Id) : IRequest<ResultT<Unit>>;