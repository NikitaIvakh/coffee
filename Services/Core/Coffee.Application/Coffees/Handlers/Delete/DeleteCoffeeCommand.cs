using Coffee.Domain.DTOs;
using MediatR;

namespace Coffee.Application.Coffees.Handlers.Delete;

public record DeleteCoffeeCommand(DeleteCoffeeDto DeleteCoffeeDto): IRequest<Unit>;