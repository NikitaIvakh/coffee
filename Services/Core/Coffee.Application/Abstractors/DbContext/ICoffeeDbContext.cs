using Microsoft.EntityFrameworkCore;
using Coffee.Domain.Entities;

namespace Coffee.Application.Abstractors.DbContext;

public interface ICoffeeDbContext
{
    public DbSet<CoffeeEntity> Coffies { get; set; }

    Task<int> SaveChangesAsync(CancellationToken token);
}