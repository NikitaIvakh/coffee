using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Providers;
using Coffee.Domain.DTOs;
using Coffee.Domain.Entities;
using Coffee.Domain.Shared;

namespace Coffee.Application.Helpers;

public class RecreateCacheAsyncHelper
{
    public static async Task RecreateCacheAsync(ICoffeeRepository coffeeRepository, ICacheProvider cacheProvider,
        CancellationToken token)
    {
        var coffeeList = await coffeeRepository.GetAllAsync();
        var coffeeEntities = coffeeList as CoffeeEntity[] ?? coffeeList.ToArray();
        for (var offset = 0; offset <= coffeeEntities.Length; offset++)
        {
            var cacheKey = $"coffees_{(offset > 0 ? offset.ToString() : "no-limit")}";
            IEnumerable<GetCoffeeListDto> coffeeListDto = coffeeEntities.Select(coffee => new GetCoffeeListDto
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

            if (offset > 0)
                coffeeListDto = coffeeListDto.Take(offset);

            var result = Result.Success(coffeeListDto.ToList());
            await cacheProvider.SetAsync(cacheKey, result, token);
        }
    }
}