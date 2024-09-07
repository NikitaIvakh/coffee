using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Helpers;
using Coffee.Application.Providers;
using Coffee.Domain.Common;
using Coffee.Domain.Shared;
using MediatR;

namespace Coffee.Application.Coffees.Handlers.Delete;

public class DeleteCoffeeCommandHandler(ICoffeeRepository coffeeRepository, IUnitOfWork unitOfWork, ICacheProvider cacheProvider)
    : IRequestHandler<DeleteCoffeeCommand, ResultT<Unit>>
{
    public async Task<ResultT<Unit>> Handle(DeleteCoffeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var coffee = await coffeeRepository.GetCoffeeEntityAsync(request.Id);

            if (!string.IsNullOrEmpty(coffee.ImageLocalPath))
            {
                var fileNameToDelete = $"Id_{coffee.Id}*";
                var filePathToDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CoffeeImages");
                var files = Directory.GetFiles(filePathToDelete, fileNameToDelete + ".*");

                foreach (var file in files)
                {
                    File.Delete(file);
                }
            }
            
            await coffeeRepository.DeleteAsync(coffee);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await cacheProvider.RemoveByPrefixAsync("coffees_", cancellationToken);
            await RecreateCacheAsyncHelper.RecreateCacheAsync(coffeeRepository, cacheProvider, cancellationToken);

            return Result.Success(Unit.Value);
        }
        
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}