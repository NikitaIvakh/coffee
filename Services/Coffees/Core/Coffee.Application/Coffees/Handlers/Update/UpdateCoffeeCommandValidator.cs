using Coffee.Domain.Common;
using FluentValidation;

namespace Coffee.Application.Coffees.Handlers.Update;

public class UpdateCoffeeCommandValidator : AbstractValidator<UpdateCoffeeCommand>
{
    public UpdateCoffeeCommandValidator()
    {
        RuleFor(key => key.UpdateCoffeeDto.Id)
            .NotNull()
            .NotEmpty();
        
        RuleFor(key => key.UpdateCoffeeDto.Name)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constraints.MaxLength)
            .MinimumLength(Constraints.MinLength);

        RuleFor(key => key.UpdateCoffeeDto.Description)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constraints.MaxLength)
            .MinimumLength(Constraints.MinLength);

        RuleFor(key => key.UpdateCoffeeDto.Price)
            .NotNull()
            .NotEmpty()
            .GreaterThanOrEqualTo(Constraints.MinValue)
            .LessThanOrEqualTo(Constraints.MaxValue);

        RuleFor(key => key.UpdateCoffeeDto.CoffeeType)
            .NotNull()
            .NotEmpty();
    }
}