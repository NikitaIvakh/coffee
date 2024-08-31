using Coffee.Domain.Interfaces;

namespace Coffee.Infrastructure.Repositories;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    public async Task<int> SaveChangesAsync(CancellationToken token)
    {
       return await context.SaveChangesAsync(token);
    }
}