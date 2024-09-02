using Coffee.Application.Providers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Coffee.Infrastructure.Interceptors;

public class CacheInvalidationInterceptor(ICacheProvider cacheProvider) : SaveChangesInterceptor
{
    public override async ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        await InvalidateData(eventData, cancellationToken);
        return result;
    }

    private async Task InvalidateData(DbContextEventData eventData, CancellationToken cancellationToken)
    {
        if (eventData.Context is null)
            return;

        var entries = eventData.Context.ChangeTracker.Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified or EntityState.Deleted);

        foreach (var entity in entries)
        {
            var entityName = entity.GetType().Name;
            await cacheProvider.RemoveByPrefixAsync(entityName, cancellationToken);
        }
    }
}