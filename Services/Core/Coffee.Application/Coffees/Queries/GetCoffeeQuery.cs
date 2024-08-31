using Coffee.Domain.DTOs;
using MediatR;

namespace Coffee.Application.Coffees.Queries;

public record GetCoffeeQuery(): IRequest<IEnumerable<GetCoffeeListDto>>;