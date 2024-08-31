using Coffee.Application.Abstractors.Interfaces;
using MediatR;

namespace Coffee.Application.Coffees.Handlers.Delete;

public class DeleteCoffeeCommandHandler(ICoffeeRepository coffeeRepository, IUnitOfWork unitOfWork)
    : IRequestHandler<DeleteCoffeeCommand, Unit>
{
    public async Task<Unit> Handle(DeleteCoffeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var coffee = await coffeeRepository.GetCoffeeEntityAsync(request.DeleteCoffeeDto.Id);

            if (coffee is null)
                throw new Exception($"Not found coffee with this id: {request.DeleteCoffeeDto.Id}");

            await coffeeRepository.DeleteAsync(coffee);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }
        
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}