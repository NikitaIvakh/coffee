using Coffee.Domain.DTOs;
using Coffee.Domain.Shared;
using MediatR;

namespace Coffee.Application.Coffees.Queries.CoffeeById;

public record GetCoffeeByIdQuery(Guid Id): IRequest<ResultT<GetCoffeeDto>>;