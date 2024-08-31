using Coffee.Domain.Entities;

namespace Coffee.Application.Abstractors.Interfaces;

public interface ICoffeeRepository
{
    IQueryable<CoffeeEntity> GetAll();

    Task CreateAsync(CoffeeEntity entity, CancellationToken token);

    Task UpdateAsync(CoffeeEntity entity);

    Task DeleteAsync(CoffeeEntity entity);
}