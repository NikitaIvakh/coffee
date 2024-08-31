using Coffee.Domain.Enums;
using Coffee.Domain.Primitives;

namespace Coffee.Domain.Entities;

public class CoffeeEntity : Entity, IAuditableData
{
    private CoffeeEntity() {}

    private CoffeeEntity(Guid id, string name, string description, decimal price, CoffeeType coffeeType, DateTimeOffset createdAt) : base(id)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        CoffeeType = coffeeType;
        CreatedAt = createdAt;
    }

    public Guid Id { get; private set; }
    
    public string Name { get; private set; } = string.Empty;
    
    public string Description { get; private set; } = string.Empty;
    
    public decimal Price { get; private set; }

    public CoffeeType CoffeeType { get; private set; }

    public DateTimeOffset CreatedAt { get; set; }

    public static CoffeeEntity Create(string name, string description, decimal price, CoffeeType coffeeType)
    {
        return new CoffeeEntity(Guid.NewGuid(), name, description, price, coffeeType, DateTimeOffset.UtcNow);
    }
    
    public static CoffeeEntity Update(CoffeeEntity existingEntity, string? name, string? description, decimal? price, CoffeeType? coffeeType)
    {
        ArgumentNullException.ThrowIfNull(existingEntity);
        existingEntity.Update(name, description, price, coffeeType);
        return existingEntity;
    }

    private void Update(string? name, string? description, decimal? price, CoffeeType? coffeeType)
    {
        if (name != null)
            Name = name;

        if (description != null)
            Description = description;

        if (price.HasValue)
            Price = price.Value;

        if (coffeeType.HasValue)
            CoffeeType = coffeeType.Value;
    }
}