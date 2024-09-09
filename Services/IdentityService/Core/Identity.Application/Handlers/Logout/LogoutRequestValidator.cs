using FluentValidation;

namespace Identity.Application.Handlers.Logout;

public class LogoutRequestValidator : AbstractValidator<LogoutRequest>
{
    public LogoutRequestValidator()
    {
        RuleFor(key => key.Id)
            .NotNull()
            .NotEmpty();
    }
}