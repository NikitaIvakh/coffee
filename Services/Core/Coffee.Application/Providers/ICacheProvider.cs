namespace Coffee.Application.Providers;

public interface ICacheProvider
{
    Task<T?> GetAsync<T>(string key, CancellationToken token = default) where T : class;
    
    Task<T> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken token = default) where T: class;

    Task SetAsync<T>(string key, T value, CancellationToken token = default) where T: class;

    Task RemoveAsync(string key, CancellationToken token = default);

    Task RemoveByPrefixAsync(string prefixKey, CancellationToken token = default);
}