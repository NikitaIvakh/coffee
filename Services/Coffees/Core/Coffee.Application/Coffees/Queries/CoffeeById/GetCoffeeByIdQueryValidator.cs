using FluentValidation;

namespace Coffee.Application.Coffees.Queries.CoffeeById;

public class GetCoffeeByIdQueryValidator : AbstractValidator<GetCoffeeByIdQuery>
{
    public GetCoffeeByIdQueryValidator()
    {
        RuleFor(key => key.Id)
            .NotNull()
            .NotEmpty();
    }
}