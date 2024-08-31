using Coffee.Application.Abstractors.Interfaces;
using Coffee.Domain.Common;
using Coffee.Domain.Entities;
using Coffee.Domain.Shared;
using MediatR;

namespace Coffee.Application.Coffees.Handlers.Create;

public class CreateCoffeeCommandHandler(ICoffeeRepository coffeeRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateCoffeeCommand, ResultT<Guid>>
{
    public async Task<ResultT<Guid>> Handle(CreateCoffeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var coffee = CoffeeEntity.Create(request.CreateCoffeeDto.Name, request.CreateCoffeeDto.Description, request.CreateCoffeeDto.Price, request.CreateCoffeeDto.CoffeeType);

            if (coffee.IsFailure)
                return Result.Failure<Guid>(DomainErrors.CoffeeEntity.CoffeeCanNotCreate($"{coffee.Error.Code}: {coffee.Error.Message}"));
            
            await coffeeRepository.CreateAsync(coffee.Value, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(coffee.Value.Id);
        }

        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}