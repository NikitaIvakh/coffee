using Coffee.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Coffee.Domain.DTOs;

public record UpdateCoffeeDto(Guid Id, string Name, string Description, decimal Price, CoffeeType CoffeeType, IFormFile? Avatar, string? ImageUrl, string? ImageLocalPath);