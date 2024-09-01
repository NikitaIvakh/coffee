using Coffee.Domain.Entities;

namespace Coffee.Application.Abstractors.Interfaces;

public interface ICoffeeRepository
{
    Task<IEnumerable<CoffeeEntity>> GetAllAsync();

    Task<CoffeeEntity> GetCoffeeEntityAsync(Guid id);

    Task<IReadOnlyList<CoffeeEntity>> GetAllWithPhotosAsync(CancellationToken token);

    Task CreateAsync(CoffeeEntity entity, CancellationToken token);

    Task UpdateAsync(CoffeeEntity entity);

    Task DeleteAsync(CoffeeEntity entity);
}