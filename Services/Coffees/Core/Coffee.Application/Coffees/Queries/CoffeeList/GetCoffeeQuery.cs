using Coffee.Domain.DTOs;
using Coffee.Domain.Shared;
using MediatR;

namespace Coffee.Application.Coffees.Queries.CoffeeList;

public record GetCoffeeQuery(string? Search, string? Filter, int Page, int PageSize, int? Limit = null) : IRequest<ResultT<PaginationList<GetCoffeeListDto>>>;