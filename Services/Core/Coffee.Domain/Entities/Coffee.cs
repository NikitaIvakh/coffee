using Coffee.Domain.Enums;
using Coffee.Domain.Primitives;

namespace Coffee.Domain.Entities;

public class Coffee : Entity, IAuditableData
{
    private Coffee() {}

    private Coffee(Guid id, string name, string description, decimal price, Sort sort, DateTimeOffset createdAt) : base(id)
    {
        Name = name;
        Description = description;
        Price = price;
        Sort = sort;
        CreatedAt = createdAt;
    }

    public string Name { get; private set; } = string.Empty;
    
    public string Description { get; private set; } = string.Empty;
    
    public decimal Price { get; private set; }
    
    public Sort Sort { get; private set; }
    
    public DateTimeOffset CreatedAt { get; set; }

    public static Coffee Create(string name, string description, decimal price, Sort sort)
    {
        return new Coffee(Guid.NewGuid(), name, description, price, sort, DateTimeOffset.UtcNow);
    }
}