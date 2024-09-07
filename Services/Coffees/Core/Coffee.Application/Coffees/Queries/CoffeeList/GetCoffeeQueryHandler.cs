using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Helpers;
using Coffee.Application.Providers;
using Coffee.Domain.DTOs;
using Coffee.Domain.Shared;
using MediatR;

namespace Coffee.Application.Coffees.Queries.CoffeeList;

public class GetCoffeeQueryHandler(ICoffeeRepository coffeeRepository, ICacheProvider cacheProvider)
    : IRequestHandler<GetCoffeeQuery, ResultT<PaginationList<GetCoffeeListDto>>>
{
    public async Task<ResultT<PaginationList<GetCoffeeListDto>>> Handle(GetCoffeeQuery request,
        CancellationToken cancellationToken)
    {
        var limitValue = request.Limit.HasValue ? request.Limit.Value.ToString() : "no-limit";
        var searchValue = request.Search ?? "no-search";
        var filterValue = request.Filter ?? "no-filter";
        var cacheKey = $"coffees_{limitValue}_{searchValue}_{filterValue}_{request.Page}_{request.PageSize}";

        return await cacheProvider.GetAsync(cacheKey, async () =>
        {
            var coffeeList = await coffeeRepository.GetAllAsync();
            IEnumerable<GetCoffeeListDto> coffeeListDto = coffeeList.Select(coffee => new GetCoffeeListDto
                (
                    coffee.Id,
                    coffee.Name,
                    CoffeeTypeHelper.GetDescription(coffee.CoffeeType),
                    coffee.Description,
                    coffee.Price,
                    coffee.CreatedAt,
                    coffee.ImageUrl,
                    coffee.ImageLocalPath
                ))
                .OrderByDescending(key => key.CreatedAt);
        
            if (request.Search is not null)
                coffeeListDto = coffeeListDto.Where(key => key.Name.Contains(request.Search));
        
            if (request.Filter is not null && request.Filter != "All")
                coffeeListDto = coffeeListDto.Where(key => string.Equals(key.CoffeeType, request.Filter));

            if (request.Filter == "All")
                coffeeListDto = coffeeListDto.ToList();
        
            if (request.Limit is > 0)
                coffeeListDto = coffeeListDto.Take(request.Limit.Value);
        
            var getCoffeeListDtos = coffeeListDto.ToList();
            var page = request.Page > 0 ? request.Page : 1;
            var pageSize = request.PageSize > 0 ? request.PageSize : 6;
        
            var coffeeListDtoPagination = await PaginationList<GetCoffeeListDto>.Create(getCoffeeListDtos, page, pageSize);
            return Result.Success(coffeeListDtoPagination);
        }, cancellationToken);
    }
}