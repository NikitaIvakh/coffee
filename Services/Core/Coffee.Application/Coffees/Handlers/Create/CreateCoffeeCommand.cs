using Coffee.Domain.DTOs;
using Coffee.Domain.Enums;
using MediatR;

namespace Coffee.Application.Coffees.Handlers.Create;

public record class CreateCoffeeCommand
    (
        CreateCoffeeDto CreateCoffeeDto
    ) : IRequest<Guid>;