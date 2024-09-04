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

        var filterOptions = coffeeEntities
            .Select(key => CoffeeTypeHelper.GetDescription(key.CoffeeType))
            .Distinct()
            .Concat(["no-filter"]);

        var searchOptions = coffeeEntities
            .Select(key => key.Name)
            .Distinct()
            .Concat(["no-search"]);

        for (var limit = 0; limit < coffeeEntities.Length; limit++)
        {
            var limitValue = limit > 0 ? limit.ToString() : "no-limit";

            foreach (var filter in filterOptions)
            {
                foreach (var search in searchOptions)
                {
                    var cacheKey = $"coffees_{limitValue}_{search}_{filter}";

                    IEnumerable<GetCoffeeListDto> coffeeListDto = coffeeEntities.Select(coffee => new GetCoffeeListDto(
                        coffee.Id,
                        coffee.Name,
                        CoffeeTypeHelper.GetDescription(coffee.CoffeeType),
                        coffee.Price,
                        coffee.CreatedAt,
                        coffee.ImageUrl,
                        coffee.ImageLocalPath
                    )).OrderByDescending(key => key.CreatedAt);

                    if (search != "no-search")
                        coffeeListDto = coffeeListDto.Where(key => key.Name.Contains(search));

                    if (filter != "no-filter")
                        coffeeListDto = coffeeListDto.Where(key => string.Equals(key.CoffeeType, filter, StringComparison.OrdinalIgnoreCase));

                    if (limit > 0)
                        coffeeListDto = coffeeListDto.Take(limit);

                    var reslult = Result.Success(coffeeListDto.ToList());
                    await cacheProvider.SetAsync(cacheKey, reslult, token);
                }
            }
        }
    }
}