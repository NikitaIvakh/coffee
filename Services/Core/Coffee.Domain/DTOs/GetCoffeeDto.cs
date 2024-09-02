namespace Coffee.Domain.DTOs;

public record GetCoffeeDto(Guid Id, string Name, string Description, decimal Price, string? ImageUrl, string? ImageLocalPath);