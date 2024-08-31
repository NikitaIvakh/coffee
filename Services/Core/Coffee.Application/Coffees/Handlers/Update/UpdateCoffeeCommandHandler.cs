using Coffee.Application.Abstractors.Interfaces;
using Coffee.Domain.Common;
using Coffee.Domain.Entities;
using Coffee.Domain.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Application.Coffees.Handlers.Update;

public class UpdateCoffeeCommandHandler(ICoffeeRepository coffeeRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCoffeeCommand, ResultT<Unit>>
{
    public async Task<ResultT<Unit>> Handle(UpdateCoffeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var coffee = await coffeeRepository.GetCoffeeEntityAsync(request.UpdateCoffeeDto.Id);

            if (coffee is null)
                return Result.Failure<Unit>(DomainErrors.CoffeeEntity.CoffeeNotFound(request.UpdateCoffeeDto.Id));

            var coffeeUpdate = CoffeeEntity.Update(coffee,
                request.UpdateCoffeeDto.Name,
                request.UpdateCoffeeDto.Description,
                request.UpdateCoffeeDto.Price,
                request.UpdateCoffeeDto.CoffeeType
            );

            if (coffeeUpdate.IsFailure)
                return Result.Failure<Unit>(DomainErrors.CoffeeEntity.CoffeeCanNotUpdate($"{coffeeUpdate.Error.Code}: {coffeeUpdate.Error.Message}"));

            await coffeeRepository.UpdateAsync(coffeeUpdate.Value);

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Result.Success(Unit.Value);
        }

        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}