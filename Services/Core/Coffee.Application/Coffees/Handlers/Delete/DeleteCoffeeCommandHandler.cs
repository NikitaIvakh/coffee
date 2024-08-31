using Coffee.Application.Abstractors.Interfaces;
using Coffee.Domain.Common;
using Coffee.Domain.Entities;
using Coffee.Domain.Shared;
using MediatR;

namespace Coffee.Application.Coffees.Handlers.Delete;

public class DeleteCoffeeCommandHandler(ICoffeeRepository coffeeRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCoffeeCommand, ResultT<Unit>>
{
    public async Task<ResultT<Unit>> Handle(DeleteCoffeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var coffee = await coffeeRepository.GetCoffeeEntityAsync(request.DeleteCoffeeDto.Id);

            if (coffee == null)
                return Result.Failure<Unit>(DomainErrors.CoffeeEntity.CoffeeCanNotDeleted(nameof(coffee)));

            await coffeeRepository.DeleteAsync(coffee);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(Unit.Value);
        }
        
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}