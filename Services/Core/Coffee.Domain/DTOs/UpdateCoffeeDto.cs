using Coffee.Domain.Enums;

namespace Coffee.Domain.DTOs;

public record UpdateCoffeeDto(Guid Id, string Name, string Description, decimal Price, CoffeeType CoffeeType);