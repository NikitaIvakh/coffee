using Coffee.Domain.DTOs;
using Coffee.Domain.Shared;
using MediatR;

namespace Coffee.Application.Coffees.Handlers.Update;

public record UpdateCoffeeCommand(UpdateCoffeeDto UpdateCoffeeDto) : IRequest<ResultT<Unit>>;