using Coffee.Domain.Shared;
using Minio.DataModel;

namespace Coffee.Application.Providers;

public interface IMinioProvider
{
    Task<ResultT<IReadOnlyList<string>>> GetPhotos(IEnumerable<string> patches, CancellationToken token);
    
    IObservable<Item> GetObjectList(CancellationToken token);
    
    Task<ResultT<string>> UploadPhoto(Stream stream, string path, CancellationToken token);

    Task<Result> RemovePhoto(string path, CancellationToken token);
    
    Task<Result> RemovePhotos(List<string> patches, CancellationToken token);
}