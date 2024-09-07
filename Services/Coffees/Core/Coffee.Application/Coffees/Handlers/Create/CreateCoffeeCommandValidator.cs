using Coffee.Domain.Common;
using FluentValidation;

namespace Coffee.Application.Coffees.Handlers.Create;

public sealed class CreateCoffeeCommandValidator : AbstractValidator<CreateCoffeeCommand>
{
    public CreateCoffeeCommandValidator()
    {
        RuleFor(key => key.CreateCoffeeDto.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constraints.MaxLength)
            .MinimumLength(Constraints.MinLength);

        RuleFor(key => key.CreateCoffeeDto.Description)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constraints.MaxLength)
            .MinimumLength(Constraints.MinLength);

        RuleFor(key => key.CreateCoffeeDto.Price)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(Constraints.MinValue)
            .LessThanOrEqualTo(Constraints.MaxValue);

        RuleFor(key => key.CreateCoffeeDto.CoffeeType)
            .NotNull()
            .NotEmpty();
    }
}