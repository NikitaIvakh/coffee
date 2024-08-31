using Coffee.Domain.DTOs;
using MediatR;

namespace Coffee.Application.Coffees.Queries.CoffeeById;

public record GetCoffeeByIdQuery(Guid Id): IRequest<GetCoffeeDto>;