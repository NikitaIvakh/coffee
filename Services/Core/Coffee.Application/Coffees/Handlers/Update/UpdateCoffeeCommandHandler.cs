using Coffee.Application.Abstractors.Interfaces;
using Coffee.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Application.Coffees.Handlers.Update;

public class UpdateCoffeeCommandHandler(ICoffeeRepository coffeeRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateCoffeeCommand, Unit>
{
    public async Task<Unit> Handle(UpdateCoffeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var coffee = await coffeeRepository.GetCoffeeEntityAsync(request.UpdateCoffeeDto.Id);

            if (coffee is null)
                throw new Exception($"Coffee with id: {request.UpdateCoffeeDto.Id} not found");

            await coffeeRepository.UpdateAsync(CoffeeEntity.Update(coffee,
                request.UpdateCoffeeDto.Name,
                request.UpdateCoffeeDto.Description, 
                request.UpdateCoffeeDto.Price,
                request.UpdateCoffeeDto.CoffeeType
            ));

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }

        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}