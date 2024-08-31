using Coffee.Domain.Entities;

namespace Coffee.Domain.Interfaces;

public interface ICoffeeRepository
{
    IQueryable<CoffeeEntity> GetAll();

    Task CreateAsync(CoffeeEntity entity);

    Task UpdateAsync(CoffeeEntity entity);

    Task DeleteAsync(CoffeeEntity entity);
}