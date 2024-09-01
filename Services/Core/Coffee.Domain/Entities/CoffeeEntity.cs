using Coffee.Domain.Common;
using Coffee.Domain.Enums;
using Coffee.Domain.Primitives;
using Coffee.Domain.Shared;

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

    public IReadOnlyCollection<CoffeePhoto> CoffeePhotos { get; private set; }
    private readonly List<CoffeePhoto> _photos = [];
    
    public DateTimeOffset CreatedAt { get; set; }

    public static ResultT<CoffeeEntity> Create(string name, string description, decimal price, CoffeeType coffeeType)
    {
        if (name.Length is > Constraints.MaxLength or < Constraints.MinLength)
            return Result.Failure<CoffeeEntity>(DomainErrors.CoffeeEntity.InvalidLength(nameof(name), name.Length));

        if (description.Length is > Constraints.MaxLength or < Constraints.MinLength)
            return Result.Failure<CoffeeEntity>(DomainErrors.CoffeeEntity.InvalidLength(nameof(description), description.Length));

        if (price is > Constraints.MaxValue or < Constraints.MinValue)
            return Result.Failure<CoffeeEntity>(DomainErrors.CoffeeEntity.InvalidValue(nameof(price)));

        var coffeeEntity = new CoffeeEntity(Guid.NewGuid(), name, description, price, coffeeType, DateTimeOffset.UtcNow);
        return Result.Create(coffeeEntity);
    }
    
    public static ResultT<CoffeeEntity> Update(CoffeeEntity existingEntity, string? name, string? description, decimal? price, CoffeeType? coffeeType)
    {
        ArgumentNullException.ThrowIfNull(existingEntity);
        existingEntity.Update(name, description, price, coffeeType);
        return Result.Success(existingEntity);
    }

    public Result AddPhoto(CoffeePhoto coffeePhoto)
    {
        if (_photos.Count > Constraints.PhotoCountLimit)
            return Result.Failure(DomainErrors.CoffeeEntity.PhotoCountLimit(nameof(coffeePhoto)));
        
        _photos.Add(coffeePhoto);
        return Result.Success();
    }

    public Result DeletePhoto(string path)
    {
        var photo = _photos.FirstOrDefault(key => key.Patch.Contains(path));

        if (photo is null)
            return Result.Failure(DomainErrors.CoffeePhoto.PhotoNotFound(nameof(path)));

        _photos.Remove(photo);
        return Result.Success();
    }

    private void Update(string? name, string? description, decimal? price, CoffeeType? coffeeType)
    {
        if (name is { Length: < Constraints.MaxLength or > Constraints.MinLength })
            Name = name;

        if (description is { Length: < Constraints.MaxLength or > Constraints.MinValue })
            Description = description;

        if (price is < Constraints.MaxValue and > Constraints.MinValue)
            Price = price.Value;

        if (coffeeType.HasValue)
            CoffeeType = coffeeType.Value;
    }
}