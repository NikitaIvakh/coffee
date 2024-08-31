using Coffee.Application.Abstractors.Interfaces;
using Coffee.Domain.DTOs;
using MediatR;

namespace Coffee.Application.Coffees.Queries.CoffeeById;

public class GetCoffeeByIdQueryHandler(ICoffeeRepository coffeeRepository)
    : IRequestHandler<GetCoffeeByIdQuery, GetCoffeeDto>
{
    public async Task<GetCoffeeDto> Handle(GetCoffeeByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var coffee = await coffeeRepository.GetCoffeeEntityAsync(request.Id);

            if (coffee is null)
                throw new Exception($"Not found coffee wih this id: {request.Id}");

            var coffeeDto = new GetCoffeeDto(coffee.Id, coffee.Name, coffee.Description, coffee.Price, coffee.ImageUrl);
            return coffeeDto;
        }
        
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}