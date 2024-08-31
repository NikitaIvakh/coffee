using Coffee.Domain.DTOs;
using MediatR;

namespace Coffee.Application.Coffees.Handlers.Update;

public record UpdateCoffeeCommand(UpdateCoffeeDto UpdateCoffeeDto) : IRequest<Unit>;