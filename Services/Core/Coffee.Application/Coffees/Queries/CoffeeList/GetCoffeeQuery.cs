using Coffee.Domain.DTOs;
using Coffee.Domain.Shared;
using MediatR;

namespace Coffee.Application.Coffees.Queries.CoffeeList;

public record GetCoffeeQuery(string? Search, string? Filter, int? Limit = null) : IRequest<ResultT<List<GetCoffeeListDto>>>;