using FluentValidation;
using Identity.Domain.Common;

namespace Identity.Application.Handlers.Register;

public sealed class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(key => key.RegisterRequestDto.FirstName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constraints.MaxLength)
            .MinimumLength(Constraints.MinLength);
        
        RuleFor(key => key.RegisterRequestDto.LastName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constraints.MaxLength)
            .MinimumLength(Constraints.MinLength);
        
        RuleFor(key => key.RegisterRequestDto.UserName)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constraints.MaxLength)
            .MinimumLength(Constraints.MinLength);
        
        RuleFor(key => key.RegisterRequestDto.EmailAddress)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constraints.MaxLength)
            .MinimumLength(Constraints.MinLength);
        
        RuleFor(key => key.RegisterRequestDto.Password)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constraints.MaxPasswordLenght)
            .MinimumLength(Constraints.MinPasswordLenght);
        
        RuleFor(key => key.RegisterRequestDto.PasswordConform)
            .NotNull()
            .NotEmpty()
            .MaximumLength(Constraints.MaxPasswordLenght)
            .MinimumLength(Constraints.MinPasswordLenght);
    }
}