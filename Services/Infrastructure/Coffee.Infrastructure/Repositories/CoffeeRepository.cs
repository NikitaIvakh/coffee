using Coffee.Application.Abstractors.Interfaces;
using Coffee.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Coffee.Infrastructure.Repositories;

public class CoffeeRepository(ApplicationDbContext context) : ICoffeeRepository
{
    public IQueryable<CoffeeEntity> GetAll()
    {
        return context.Coffies.AsNoTracking().AsQueryable();
    }

    public async Task CreateAsync(CoffeeEntity entity, CancellationToken token)
    {
        if (entity is null)
            throw new Exception("Entity is error");

        await context.AddAsync(entity, token);
        await Task.CompletedTask;
    }

    public async Task UpdateAsync(CoffeeEntity entity)
    {
        if (entity is null)
            throw new Exception("Entity is error");

        context.Update(entity);
        await Task.CompletedTask;
    }

    public async Task DeleteAsync(CoffeeEntity entity)
    {
        if (entity is null)
            throw new Exception("Entity is error");

        context.Remove(entity);
        await Task.CompletedTask;
    }
}