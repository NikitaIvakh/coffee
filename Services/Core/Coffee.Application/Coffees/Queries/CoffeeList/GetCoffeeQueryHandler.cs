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
        var limitValue = request.Offset.HasValue ? request.Offset.Value.ToString() : "no-limit";
        var cacheKey = $"coffees_{limitValue}";
        return await cacheProvider.GetAsync(cacheKey, async () =>
        {
            var coffeeList = await coffeeRepository.GetAllAsync();
            IEnumerable<GetCoffeeListDto> coffeeListDto = coffeeList.Select(coffee => new GetCoffeeListDto
                (
                    coffee.Id,
                    coffee.Name,
                    coffee.CoffeeType,
                    coffee.Price,
                    coffee.CreatedAt,
                    coffee.ImageUrl,
                    coffee.ImageLocalPath
                ))
                .OrderByDescending(key => key.CreatedAt);

            if (request.Offset is > 0)
                coffeeListDto = coffeeListDto.Take(request.Offset.Value);

            return Result.Success(coffeeListDto.ToList());
        }, cancellationToken);
    }
}