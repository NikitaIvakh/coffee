using Coffee.Domain.Enums;
using Coffee.Domain.Primitives;

namespace Coffee.Domain.Entities;

public class CoffeeEntity : Entity, IAuditableData
{
    private CoffeeEntity() {}

    private CoffeeEntity(Guid id, string name, string description, decimal price, Sort sort, DateTimeOffset createdAt) : base(id)
    {
        Id = id;
        Name = name;
        Description = description;
        Price = price;
        Sort = sort;
        CreatedAt = createdAt;
    }

    public Guid Id { get; private set; }
    
    public string Name { get; private set; } = string.Empty;
    
    public string Description { get; private set; } = string.Empty;
    
    public decimal Price { get; private set; }
    
    public Sort Sort { get; private set; }
    
    public DateTimeOffset CreatedAt { get; set; }

    public static CoffeeEntity Create(string name, string description, decimal price, Sort sort)
    {
        return new CoffeeEntity(Guid.NewGuid(), name, description, price, sort, DateTimeOffset.UtcNow);
    }
}