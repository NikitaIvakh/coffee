using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Providers;
using Coffee.Domain.DTOs;
using Coffee.Domain.Entities;
using Coffee.Domain.Shared;

namespace Coffee.Application.Helpers;

public abstract class RecreateCacheAsyncHelper
{
    public static async Task RecreateCacheAsync(ICoffeeRepository coffeeRepository, ICacheProvider cacheProvider, CancellationToken token)
    {
        var coffeeEntities = coffeeRepository.GetAllAsync().Result.ToArray();
        var filterOptions = GetFilterOptions(coffeeEntities);

        var searchOptions = GetSearchOptions(coffeeEntities);
        var searchEnumerable = searchOptions as string[] ?? searchOptions.ToArray();

        foreach (var filter in filterOptions)
        {
            foreach (var search in searchEnumerable)
            {
                var cacheKey = GenerateCacheKey(filter, search);
                var coffeeListDto = GetFilteredAndSearchedCoffeeListDto(coffeeEntities, search, filter);

                var result = Result.Success(coffeeListDto);
                await cacheProvider.SetAsync(cacheKey, result, token);
            }
        }
    }

    private static IEnumerable<string> GetFilterOptions(CoffeeEntity[] coffeeEntities)
    {
        return coffeeEntities
            .Select(key => CoffeeTypeHelper.GetDescription(key.CoffeeType))
            .Distinct()
            .Append("no-filter");
    }

    private static IEnumerable<string> GetSearchOptions(CoffeeEntity[] coffeeEntities)
    {
        return coffeeEntities
            .Select(key => key.Name)
            .Distinct()
            .Append("no-search");
    }

    private static string GenerateCacheKey(string filter, string search)
    {
        return $"coffees_{filter}_{search}";
    }

    private static List<GetCoffeeListDto> GetFilteredAndSearchedCoffeeListDto(CoffeeEntity[] coffeeEntities,
        string search, string filter)
    {
        var coffeeListDto = coffeeEntities.Select(coffee =>
            new GetCoffeeListDto(
                coffee.Id,
                coffee.Name,
                CoffeeTypeHelper.GetDescription(coffee.CoffeeType),
                coffee.Price,
                coffee.CreatedAt,
                coffee.ImageUrl,
                coffee.ImageLocalPath
            )).OrderByDescending(key => key.CreatedAt).ToList();

        if (search != "no-search")
            coffeeListDto = coffeeListDto.Where(key => key.Name.Contains(search)).ToList();

        if (filter != "no-filter")
            coffeeListDto = coffeeListDto.Where(key => string.Equals(key.CoffeeType, filter)).ToList();

        return coffeeListDto;
    }
}