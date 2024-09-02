using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Providers;
using Coffee.Domain.DTOs;
using Coffee.Domain.Shared;
using MediatR;

namespace Coffee.Application.Coffees.Queries.CoffeeList;

public class GetCoffeeQueryHandler(ICoffeeRepository coffeeRepository, ICacheProvider cacheProvider)
    : IRequestHandler<GetCoffeeQuery, ResultT<List<GetCoffeeListDto>>>
{
    public async Task<ResultT<List<GetCoffeeListDto>>> Handle(GetCoffeeQuery request,
        CancellationToken cancellationToken)
    {
        return await cacheProvider.GetAsync("coffees", async () =>
        {
            var coffeeList = await coffeeRepository.GetAllAsync();
            var coffeeListDto = coffeeList.Select(coffee => new GetCoffeeListDto
                (
                    coffee.Id,
                    coffee.Name,
                    coffee.CoffeeType,
                    coffee.Price,
                    coffee.CreatedAt,
                    coffee.ImageUrl,
                    coffee.ImageLocalPath
                ))
                .OrderByDescending(key => key.CreatedAt)
                .ToList();

            return Result.Success(coffeeListDto);
        }, cancellationToken);
    }
}