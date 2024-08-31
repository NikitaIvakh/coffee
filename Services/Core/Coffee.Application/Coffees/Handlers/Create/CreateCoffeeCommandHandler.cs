using Coffee.Application.Abstractors.Interfaces;
using Coffee.Domain.Entities;
using MediatR;

namespace Coffee.Application.Coffees.Handlers.Create;

public class CreateCoffeeCommandHandler(ICoffeeRepository coffeeRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCoffeeCommand, Guid>
{
    public async Task<Guid> Handle(CreateCoffeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var coffee = CoffeeEntity.Create(request.CreateCoffeeDto.Name, request.CreateCoffeeDto.Description, request.CreateCoffeeDto.Price, request.CreateCoffeeDto.CoffeeType);
            
            await coffeeRepository.CreateAsync(coffee, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return coffee.Id;
        }

        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}