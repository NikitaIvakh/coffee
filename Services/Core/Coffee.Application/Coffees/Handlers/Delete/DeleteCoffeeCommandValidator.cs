using FluentValidation;

namespace Coffee.Application.Coffees.Handlers.Delete;

public class DeleteCoffeeCommandValidator : AbstractValidator<DeleteCoffeeCommand>
{
    public DeleteCoffeeCommandValidator()
    {
        RuleFor(key => key.Id)
            .NotNull()
            .NotEmpty();
    }
}