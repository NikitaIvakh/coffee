﻿using Coffee.Domain.DTOs;
using Coffee.Domain.Shared;
using MediatR;

namespace Coffee.Application.Coffees.Handlers.Delete;

public record DeleteCoffeeCommand(Guid Id): IRequest<ResultT<Unit>>;