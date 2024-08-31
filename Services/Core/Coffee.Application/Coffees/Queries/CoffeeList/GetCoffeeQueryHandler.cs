using Coffee.Application.Abstractors.Interfaces;
using Coffee.Domain.DTOs;
using MediatR;

namespace Coffee.Application.Coffees.Queries.CoffeeList;

public class GetCoffeeQueryHandler(ICoffeeRepository coffeeRepository)
    : IRequestHandler<GetCoffeeQuery, IEnumerable<GetCoffeeListDto>>
{
    public async Task<IEnumerable<GetCoffeeListDto>> Handle(GetCoffeeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var coffeeList = await coffeeRepository.GetAllAsync();
            var coffeeListDto = coffeeList.Select(coffee => new GetCoffeeListDto
            (
                coffee.Name, 
                coffee.CoffeeType, 
                coffee.Price, 
                coffee.ImageUrl, 
                coffee.CreatedAt)
            ).OrderByDescending(key => key.CreatedAt)
                .ToList();

            return coffeeListDto;
        }
        
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}