using FluentValidation;

namespace Identity.Application.Handlers.Logout;

public sealed class LogoutRequestValidator : AbstractValidator<LogoutRequest>
{
    public LogoutRequestValidator()
    {
        RuleFor(key => key.Id)
            .NotNull()
            .NotEmpty();
    }
}