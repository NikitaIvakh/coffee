using Coffee.Domain.Common;
using Coffee.Domain.Enums;
using Coffee.Domain.Primitives;
using Coffee.Domain.Shared;

namespace Coffee.Domain.Entities;

public class CoffeeEntity : Entity, IAuditableData
{
    private CoffeeEntity() { }

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

    public string? ImageUrl { get; private set; }

    public string? ImageLocalPath { get; private set; }

    public CoffeeType CoffeeType { get; private set; }

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

    public static ResultT<CoffeeEntity> Update(CoffeeEntity existingEntity, string? name, string? description,
        decimal? price, CoffeeType? coffeeType)
    {
        ArgumentNullException.ThrowIfNull(existingEntity);
        existingEntity.Update(name, description, price, coffeeType);
        return Result.Success(existingEntity);
    }

    public static void UpdateImage(CoffeeEntity coffeeEntity, string? imageUrl, string? imageLocalPath)
    {
        if (imageUrl is null && imageLocalPath is null)
        {
            Result.Failure<CoffeeEntity>(DomainErrors.CoffeePhoto.SaveFailure($"{nameof(imageUrl)}: {imageLocalPath}"));
            return;
        }

        if (imageUrl is not null || imageLocalPath is not null)
            UpdateImageFields(coffeeEntity, imageUrl, imageLocalPath);

        Result.Success(coffeeEntity);
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

    private static void UpdateImageFields(CoffeeEntity coffeeEntity, string? imageUrl, string? imageLocalPath)
    {
        if (imageUrl is not null)
            coffeeEntity.ImageUrl = imageUrl;

        if (imageLocalPath is not null)
            coffeeEntity.ImageLocalPath = imageLocalPath;

        if (imageUrl is null && imageLocalPath is null)
        {
            coffeeEntity.ImageUrl = "https://placehold.co/600x400";
            coffeeEntity.ImageLocalPath = "https://placehold.co/600x400";
        }
    }
}