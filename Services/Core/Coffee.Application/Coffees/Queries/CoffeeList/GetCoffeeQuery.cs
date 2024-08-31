using Coffee.Domain.DTOs;
using MediatR;

namespace Coffee.Application.Coffees.Queries.CoffeeList;

public record GetCoffeeQuery(): IRequest<IEnumerable<GetCoffeeListDto>>;