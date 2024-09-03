using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Helpers;
using Coffee.Application.Providers;
using Coffee.Domain.Common;
using Coffee.Domain.Entities;
using Coffee.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Application.Coffees.Handlers.Update;

public class UpdateCoffeeCommandHandler(ICoffeeRepository coffeeRepository, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor, ICacheProvider cacheProvider)
    : IRequestHandler<UpdateCoffeeCommand, ResultT<Unit>>
{
    public async Task<ResultT<Unit>> Handle(UpdateCoffeeCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var coffee = await coffeeRepository.GetCoffeeEntityAsync(request.UpdateCoffeeDto.Id);

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

            if (request.UpdateCoffeeDto.Avatar is not null)
            {
                if (!string.IsNullOrEmpty(coffee.ImageLocalPath))
                {
                    var fileNameToDelete = $"Id_{coffee.Id}*";
                    var filePathToDelete = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CoffeeImages");
                    var files = Directory.GetFiles(filePathToDelete, fileNameToDelete + ".*");

                    foreach (var file in files)
                    {
                        File.Delete(file);
                    }
                    
                    CoffeeEntity.UpdateImage(coffee, null, null);
                }

                var fileName = $"Id_{coffee.Id}------${Guid.NewGuid()}" +
                               Path.GetExtension(request.UpdateCoffeeDto.Avatar.FileName);
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "CoffeeImages");
                var directoryLocation = Path.Combine(Directory.GetCurrentDirectory(), filePath);

                if (!Directory.Exists(filePath))
                    Directory.CreateDirectory(filePath);

                var fileFullPath = Path.Combine(directoryLocation, fileName);

                await using (FileStream fileStream = new(fileFullPath, FileMode.Create))
                    await request.UpdateCoffeeDto.Avatar.CopyToAsync(fileStream, cancellationToken);

                var baseUrl = $"{httpContextAccessor.HttpContext!.Request.Scheme}://" +
                              $"${httpContextAccessor.HttpContext.Request.Host.Value}" +
                              $"${httpContextAccessor.HttpContext.Request.PathBase.Value}";
                
                CoffeeEntity.UpdateImage(coffee, Path.Combine(baseUrl, "CoffeeImages", fileName), filePath);
            }

            await coffeeRepository.UpdateAsync(coffeeUpdate.Value);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await RecreateCacheAsyncHelper.RecreateCacheAsync(coffeeRepository, cacheProvider, cancellationToken);
            
            return Result.Success(Unit.Value);
        }

        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }
}