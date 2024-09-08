using Identity.Application.Abstractors.Interfaces;

namespace Identity.Infrastructure.Repository;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken token = default)
    {
        return await context.SaveChangesAsync(token);
    }
}