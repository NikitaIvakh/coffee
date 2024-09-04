using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Helpers;
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
        var limitValue = request.Limit.HasValue ? request.Limit.Value.ToString() : "no-limit";
        var searchValue = request.Search ?? "no-search";
        var filterValue = request.Filter ?? "no-filter";
        var cacheKey = $"coffees_{limitValue}_{searchValue}_{filterValue}"; 
        
        return await cacheProvider.GetAsync(cacheKey, async () =>
        {
            var coffeeList = await coffeeRepository.GetAllAsync();
            IEnumerable<GetCoffeeListDto> coffeeListDto = coffeeList.Select(coffee => new GetCoffeeListDto
                (
                    coffee.Id,
                    coffee.Name,
                    CoffeeTypeHelper.GetDescription(coffee.CoffeeType),
                    coffee.Price,
                    coffee.CreatedAt,
                    coffee.ImageUrl,
                    coffee.ImageLocalPath
                ))
                .OrderByDescending(key => key.CreatedAt);

            if (request.Search is not null)
                coffeeListDto= coffeeListDto.Where(key => key.Name.Contains(request.Search));

            if (request.Filter is not null)
                coffeeListDto = coffeeListDto.Where(key => string.Equals(key.CoffeeType, request.Filter, StringComparison.OrdinalIgnoreCase));

            if (request.Limit is > 0)
                coffeeListDto = coffeeListDto.Take(request.Limit.Value);

            return Result.Success(coffeeListDto.ToList());
        }, cancellationToken);
    }
}