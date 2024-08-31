using Coffee.Domain.DTOs;
using Coffee.Domain.Shared;
using MediatR;

namespace Coffee.Application.Coffees.Queries.CoffeeList;

public record GetCoffeeQuery(): IRequest<ResultT<List<GetCoffeeListDto>>>;