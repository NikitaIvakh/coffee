﻿using Coffee.Domain.Enums;

namespace Coffee.Domain.DTOs;

public record GetCoffeeListDto(Guid Id, string Name, CoffeeType CoffeeType, decimal Price, DateTimeOffset CreatedAt, string? ImageUrl, string? ImageLocalPath);