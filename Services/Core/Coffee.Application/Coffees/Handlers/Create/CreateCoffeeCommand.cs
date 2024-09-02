using Coffee.Domain.DTOs;
using Coffee.Domain.Enums;
using Coffee.Domain.Shared;
using MediatR;

namespace Coffee.Application.Coffees.Handlers.Create;

public record CreateCoffeeCommand(CreateCoffeeDto CreateCoffeeDto) : IRequest<ResultT<Guid>>;