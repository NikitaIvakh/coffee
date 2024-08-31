using Coffee.Domain.Enums;

namespace Coffee.Domain.DTOs;

public class UpdateCoffeeDto(Guid id, string name, string description, decimal price, CoffeeType coffeeType)
{
    public Guid Id { get; init; } = id;

    public string Name { get; init; } = name;

    public string Description { get; init; } = description;

    public decimal Price { get; init; } = price;

    public CoffeeType CoffeeType { get; init; } = coffeeType;
}