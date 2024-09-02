using Coffee.Domain.Enums;
using Microsoft.AspNetCore.Http;

namespace Coffee.Domain.DTOs;

public record CreateCoffeeDto(string Name, string Description, decimal Price, CoffeeType CoffeeType, IFormFile? Avatar);