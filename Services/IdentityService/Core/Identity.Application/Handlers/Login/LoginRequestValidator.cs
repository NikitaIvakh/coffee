using FluentValidation;
using Identity.Domain.Common;

namespace Identity.Application.Handlers.Login;

public sealed class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(key => key.LoginRequestDto.EmailAddress)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constraints.MaxLength)
            .MinimumLength(Constraints.MinLength);

        RuleFor(key => key.LoginRequestDto.Password)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constraints.MaxPasswordLenght)
            .MinimumLength(Constraints.MinPasswordLenght);
    }
}