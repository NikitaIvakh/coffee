using Coffee.Domain.Enums;

namespace Coffee.Domain.DTOs;

public record GetCoffeeListDto(string Name, CoffeeType CoffeeType, decimal Price, string ImageUrl, DateTimeOffset CreatedAt);