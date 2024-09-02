using System.Collections.Concurrent;
using Coffee.Application.Providers;
using Microsoft.Extensions.Caching.Distributed;
using Newtonsoft.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace Coffee.Infrastructure.Providers;

public class CacheProvider(IDistributedCache distributedCache) : ICacheProvider
{
    private static readonly ConcurrentDictionary<string, bool> CacheKeys = new();

    public async Task<T?> GetAsync<T>(string key, CancellationToken token = default) where T : class
    {
        var cachedValue = await distributedCache.GetStringAsync(key, token);

        if (cachedValue is null)
            return null;

        var value = JsonConvert.DeserializeObject<T>(cachedValue);
        return value;
    }
    
    public async Task<T> GetAsync<T>(string key, Func<Task<T>> factory, CancellationToken token = default) where T: class
    {
        var cacheValue = await GetAsync<T>(key, token);

        if (cacheValue is not null)
            return cacheValue;

        cacheValue = await factory();
        await SetAsync(key, cacheValue, token);
        
        return cacheValue;
    }

    public async Task SetAsync<T>(string key, T value, CancellationToken token = default) where T: class
    {
        var stringValue = JsonConvert.SerializeObject(value);
        await distributedCache.SetStringAsync(key, stringValue, token);
        CacheKeys.TryAdd(key, true);
    }

    public async Task RemoveAsync(string key, CancellationToken token = default)
    {
        await distributedCache.RemoveAsync(key, token);
        CacheKeys.TryRemove(key, out _);
    }

    public async Task RemoveByPrefixAsync(string prefixKey, CancellationToken token = default)
    {
        var tasks = CacheKeys.Keys
            .Where(key => key.StartsWith(prefixKey))
            .Select(key => RemoveAsync(key, token));

        await Task.WhenAll(tasks);
    }
}