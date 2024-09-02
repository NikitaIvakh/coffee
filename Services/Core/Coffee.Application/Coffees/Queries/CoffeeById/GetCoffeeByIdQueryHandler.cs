using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Providers;
using Coffee.Domain.DTOs;
using Coffee.Domain.Shared;
using MediatR;

namespace Coffee.Application.Coffees.Queries.CoffeeById;

public class GetCoffeeByIdQueryHandler(ICoffeeRepository coffeeRepository, ICacheProvider cacheProvider)
    : IRequestHandler<GetCoffeeByIdQuery, ResultT<GetCoffeeDto>>
{
    public async Task<ResultT<GetCoffeeDto>> Handle(GetCoffeeByIdQuery request, CancellationToken cancellationToken)
    {
        return await cacheProvider.GetAsync("CoffeeById", async () =>
        {
            var coffee = await coffeeRepository.GetCoffeeEntityAsync(request.Id);
            var coffeeDto = new GetCoffeeDto(coffee.Id, coffee.Name, coffee.Description, coffee.Price, coffee.ImageUrl, coffee.ImageLocalPath);

            return Result.Success(coffeeDto);
        }, cancellationToken);
    }
}