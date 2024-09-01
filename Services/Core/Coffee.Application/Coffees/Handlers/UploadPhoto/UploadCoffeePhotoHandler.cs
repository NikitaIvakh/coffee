using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Providers;
using Coffee.Domain.Common;
using Coffee.Domain.Entities;
using Coffee.Domain.Shared;

namespace Coffee.Application.Coffees.Handlers.UploadPhoto;

public class UploadCoffeePhotoHandler(
    ICoffeeRepository coffeeRepository,
    IMinioProvider minioProvider,
    IUnitOfWork unitOfWork)
{
    public async Task<ResultT<string>> Handle(UploadCoffeePhotoRequest request, CancellationToken token)
    {
        var coffee = await coffeeRepository.GetCoffeeEntityAsync(request.Id);

        if (coffee is null)
            return Result.Failure<string>(DomainErrors.CoffeeEntity.CoffeeNotFound(request.Id));

        var photoId = Guid.NewGuid();
        var path = photoId + Path.GetExtension(request.File.FileName);

        var photo = CoffeePhoto.CreateAndActive(path, request.File.ContentType, request.File.Length, request.IsMain);

        if (photo.IsFailure)
            return Result.Failure<string>(DomainErrors.CoffeePhoto.SaveFailure(nameof(photo)));

        var isSuccessUpload = coffee.AddPhoto(photo.Value);

        if (isSuccessUpload.IsFailure)
            return Result.Failure<string>(DomainErrors.CoffeePhoto.SaveFailure(nameof(isSuccessUpload)));

        await using var stream = request.File.OpenReadStream();
        var objectName = await minioProvider.UploadPhoto(stream, path, token);

        if (objectName.IsFailure)
            return Result.Failure<string>(DomainErrors.CoffeePhoto.SaveFailure(nameof(isSuccessUpload)));

        await unitOfWork.SaveChangesAsync(token);

        return Result.Success(path);
    }
}