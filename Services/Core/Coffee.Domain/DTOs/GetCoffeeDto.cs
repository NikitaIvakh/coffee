namespace Coffee.Domain.DTOs;

public record GetCoffeeDto(Guid Id, string Name, string CoffeeType, string Description, decimal Price, string? ImageUrl, string? ImageLocalPath);