using Identity.Domain.Shared;
using MediatR;

namespace Identity.Application.Handlers.VerifyEmail;

public record VerifyEmailRequest(string Id) : IRequest<ResultT<bool>>;
