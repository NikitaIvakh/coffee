using System.Reflection;
using Coffee.Application.Abstractors.DbContext;
using Coffee.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Infrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options), ICoffeeDbContext
{
    public DbSet<CoffeeEntity> Coffies { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
    }
}