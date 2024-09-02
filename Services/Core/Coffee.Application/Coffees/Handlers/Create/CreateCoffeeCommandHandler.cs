using Coffee.Application.Abstractors.Interfaces;
using Coffee.Domain.Common;
using Coffee.Domain.Entities;
using Coffee.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Coffee.Application.Coffees.Handlers.Create;

public class CreateCoffeeCommandHandler(ICoffeeRepository coffeeRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
    : IRequestHandler<CreateCoffeeCommand, ResultT<Guid>>
{
    public async Task<ResultT<Guid>> Handle(CreateCoffeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var coffee = CoffeeEntity.Create
            (
                request.CreateCoffeeDto.Name, 
                request.CreateCoffeeDto.Description, 
                request.CreateCoffeeDto.Price, 
                request.CreateCoffeeDto.CoffeeType
            );

            if (coffee.IsFailure)
                return Result.Failure<Guid>(DomainErrors.CoffeeEntity.CoffeeCanNotCreate($"{coffee.Error.Code}: {coffee.Error.Message}"));
            
            await coffeeRepository.CreateAsync(coffee.Value, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            if (request.CreateCoffeeDto.Avatar is not null)
            {
                var fileName = $"Id_{coffee.Value.Id}------{Guid.NewGuid()}" + Path.GetExtension(request.CreateCoffeeDto.Avatar.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CoffeeImages");
                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                FileInfo fileInfo = new(directoryLocation);
                
                if (fileInfo.Exists)
                    fileInfo.Delete();

                var fileDirectory = Path.Combine(filePath, fileName);

                await using (FileStream fileStream = new(fileDirectory, FileMode.Create))
                    await request.CreateCoffeeDto.Avatar.CopyToAsync(fileStream, cancellationToken);

                var baseUrl = $"{httpContextAccessor.HttpContext!.Request.Scheme}://" +
                              $"{httpContextAccessor.HttpContext.Request.Host.Value}" +
                              $"{httpContextAccessor.HttpContext.Request.PathBase.Value}";

                CoffeeEntity.UpdateImage(coffee.Value, Path.Combine(baseUrl, "CoffeeImages", fileName), filePath);
            }
            
            await coffeeRepository.UpdateAsync(coffee.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success(coffee.Value.Id);
        }

        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}