﻿using Coffee.Domain.Enums;

namespace Coffee.Domain.DTOs;

public record GetCoffeeListDto(Guid Id, string Name, string CoffeeType, string Description, decimal Price, DateTimeOffset CreatedAt, string? ImageUrl, string? ImageLocalPath);