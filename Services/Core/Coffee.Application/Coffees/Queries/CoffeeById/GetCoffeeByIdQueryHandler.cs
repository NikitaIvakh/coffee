using Coffee.Application.Abstractors.Interfaces;
using Coffee.Domain.Common;
using Coffee.Domain.DTOs;
using Coffee.Domain.Shared;
using MediatR;

namespace Coffee.Application.Coffees.Queries.CoffeeById;

public class GetCoffeeByIdQueryHandler(ICoffeeRepository coffeeRepository)
    : IRequestHandler<GetCoffeeByIdQuery, ResultT<GetCoffeeDto>>
{
    public async Task<ResultT<GetCoffeeDto>> Handle(GetCoffeeByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var coffee = await coffeeRepository.GetCoffeeEntityAsync(request.Id);

            if (coffee is null)
                return Result.Failure<GetCoffeeDto>(DomainErrors.CoffeeEntity.CoffeeNotFound(request.Id));

            var coffeeDto = new GetCoffeeDto(coffee.Id, coffee.Name, coffee.Description, coffee.Price, coffee.ImageUrl);
            return Result.Success(coffeeDto);
        }
        
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}