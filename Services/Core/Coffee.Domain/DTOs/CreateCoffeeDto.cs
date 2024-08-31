using Coffee.Domain.Enums;

namespace Coffee.Domain.DTOs;

public class CreateCoffeeDto(string name, string description, decimal price, CoffeeType coffeeType)
{
    public string Name { get; init; } = name;

    public string Description { get; init; } = description;

    public decimal Price { get; init; } = price;

    public CoffeeType CoffeeType { get;  init; } = coffeeType;
}