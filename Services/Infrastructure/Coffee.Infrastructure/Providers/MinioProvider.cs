using Coffee.Application.Providers;
using Coffee.Domain.Common;
using Coffee.Domain.Shared;
using Microsoft.Extensions.Logging;
using Minio;
using Minio.ApiEndpoints;
using Minio.DataModel;
using Minio.DataModel.Args;

namespace Coffee.Infrastructure.Providers;

public class MinioProvider(IMinioClient minioClient, ILogger<MinioProvider> logger) : IMinioProvider
{
    private const string PhotoBucket = "images";

    public async Task<ResultT<IReadOnlyList<string>>> GetPhotos(IEnumerable<string> patches, CancellationToken token)
    {
        try
        {
            List<string> urls = [];

            foreach (var path in patches)
            {
                var presignedGetObjectArgs = new PresignedGetObjectArgs()
                    .WithBucket(PhotoBucket)
                    .WithObject(path)
                    .WithExpiry(60 * 60 * 24);
                
                var url = await minioClient.PresignedGetObjectAsync(presignedGetObjectArgs);
                urls.Add(url);
            }

            return Result.Create<IReadOnlyList<string>>(urls);
        }
        
        catch (Exception exception)
        {
            logger.LogError($"Error while loading photos in minio: {exception.Message}");
            return Result.Failure<IReadOnlyList<string>>(DomainErrors.CoffeePhoto.PhotosLoadingFailure(nameof(patches)));
        }
    }
    
    [Obsolete("Obsolete")]
    public IObservable<Item> GetObjectList(CancellationToken token)
    {
        var listObjectArgs = new ListObjectsArgs().WithBucket(PhotoBucket);
        return minioClient.ListObjectsAsync(listObjectArgs);
    }

    public async Task<ResultT<string>> UploadPhoto(Stream stream, string path, CancellationToken token)
    {
        try
        {
            var bucketExistsArgs = new BucketExistsArgs().WithBucket(PhotoBucket);
            var bucketExists = await minioClient.BucketExistsAsync(bucketExistsArgs, token);

            if (bucketExists is false)
            {
                var makeBucketArgs = new MakeBucketArgs().WithBucket(PhotoBucket);
                await minioClient.MakeBucketAsync(makeBucketArgs, token);
            }

            var putObjectArgs = new PutObjectArgs()
                .WithBucket(PhotoBucket)
                .WithStreamData(stream)
                .WithObjectSize(stream.Length)
                .WithObject(path);

            var response = await minioClient.PutObjectAsync(putObjectArgs, token);
            return Result.Success(response.ObjectName);
        }
        
        catch (Exception exception)
        {
           logger.LogError($"Error while saving file in minio: {exception.Message}");
           return Result.Failure<string>(DomainErrors.CoffeePhoto.SaveFailure(nameof(path)));
        }
    }

    public async Task<Result> RemovePhoto(string path, CancellationToken token)
    {
        try
        {
            var bucketExistArgs = new BucketExistsArgs().WithBucket(PhotoBucket);
            var bucketExists = await minioClient.BucketExistsAsync(bucketExistArgs, token);

            if (bucketExists is false)
            {
                var makeBucketArgs = new MakeBucketArgs().WithBucket(PhotoBucket);
                await minioClient.MakeBucketAsync(makeBucketArgs, token);
            }

            var removeObjectArgs = new RemoveObjectArgs().WithBucket(PhotoBucket).WithObject(path);
            await minioClient.RemoveObjectAsync(removeObjectArgs, token);
            return Result.Success();
        }
        
        catch (Exception exception)
        {
            logger.LogError($"Error while deleting file in minio: {exception.Message}");
            return Result.Failure(DomainErrors.CoffeePhoto.RemovePhotoFailure(nameof(path)));
        }
    }

    public async Task<Result> RemovePhotos(List<string> patches, CancellationToken token)
    {
        try
        {
            var bucketExistsArgs = new BucketExistsArgs().WithBucket(PhotoBucket);
            var bucketExists = await minioClient.BucketExistsAsync(bucketExistsArgs, token);

            if (bucketExists is false)
            {
                var makeBucketArgs = new MakeBucketArgs().WithBucket(PhotoBucket);
                await minioClient.MakeBucketAsync(makeBucketArgs, token);
            }

            var removeObjectArgs = new RemoveObjectsArgs().WithBucket(PhotoBucket).WithObjects(patches);
            await minioClient.RemoveObjectsAsync(removeObjectArgs, token);
            return Result.Success();
        }
        
        catch (Exception exception)
        {
            logger.LogError($"Error while deleting files in minio: {exception.Message}");
            return Result.Failure(DomainErrors.CoffeePhoto.RemovePhotosFailure(nameof(patches)));
        }
    }
}