using FluentValidation;

namespace Identity.Application.Handlers.RefreshToken;

public class RefreshTokenValidator: AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenValidator()
    {
        RuleFor(key => key.TokenModelDto.AccessToken)
            .NotNull()
            .NotEmpty();

        RuleFor(key => key.TokenModelDto.RefreshToken)
            .NotNull()
            .NotEmpty();
    }
}