using FluentValidation;

namespace Identity.Application.Handlers.RevokeToken;

public class RevokeTokenValidator : AbstractValidator<RevokeTokenRequest>
{
    public RevokeTokenValidator()
    {
        RuleFor(key => key.Id)
            .NotNull()
            .NotEmpty();
    }   
}