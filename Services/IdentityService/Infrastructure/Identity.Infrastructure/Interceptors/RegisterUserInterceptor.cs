using Identity.Domain.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Identity.Infrastructure.Interceptors;

public sealed class RegisterUserInterceptor : SaveChangesInterceptor
{
    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        var context = eventData.Context;

        if (context is null)
            return base.SavingChangesAsync(eventData, result, cancellationToken);

        foreach (var entity in context.ChangeTracker.Entries<IAuditableData>()
                     .Where(key => key.State == EntityState.Added))
        {
            if (entity.State == EntityState.Added)
                entity.Property(key => key.CreatedAt).CurrentValue = DateTimeOffset.UtcNow;
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}