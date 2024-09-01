using System.Reactive.Linq;
using Coffee.Application.Abstractors.Interfaces;
using Coffee.Application.Providers;
using Microsoft.Extensions.Logging;

namespace Coffee.Infrastructure.Jobs;

public class ImageCleanUpJob(
    IMinioProvider minioProvider,
    ICoffeeRepository coffeeRepository,
    ILogger<ImageCleanUpJob> logger)
    : IImageCleanUpJob
{
    public async Task ProcessAsync()
    {
        var token = new CancellationTokenSource().Token;
        logger.LogInformation("Cleaning up unused images...");

        List<string> storagePhotoPaths = [];

        var objectList = minioProvider.GetObjectList(token);

        objectList.Subscribe(item => storagePhotoPaths.Add(item.Key));
        await objectList.LastOrDefaultAsync();

        if (storagePhotoPaths.Count == 0)
        {
            logger.LogInformation("No images to delete");
            return;
        }

        var coffees = await coffeeRepository.GetAllWithPhotosAsync(token);

        var photoPaths = coffees
            .SelectMany(key => key.CoffeePhotos)
            .Select(key => key.Patch)
            .ToList();

        var extraPaths = storagePhotoPaths
            .Except(photoPaths)
            .ToList();

        if (extraPaths.Count == 0)
        {
            logger.LogInformation("Ni images to delete");
            return;
        }

        var removeResult = await minioProvider.RemovePhotos(extraPaths, token);
        
        if(removeResult.IsFailure)
            logger.LogError("Error deleting images from MinIO storage");
        
        logger.LogInformation("Images has been deleted fro MinIO storage");
    }
}