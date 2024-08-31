using Coffee.Domain.Enums;

namespace Coffee.Domain.DTOs;

public record CreateCoffeeDto(string Name, string Description, decimal Price, CoffeeType CoffeeType);