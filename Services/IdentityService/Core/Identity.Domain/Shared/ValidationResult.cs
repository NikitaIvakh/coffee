namespace Identity.Domain.Shared;

public class ValidationResult(Error[] errors) : IValidationResult
{
    public Error[] Errors { get; } = errors;

    public static ValidationResult WithErrors(Error[] errors) => new(errors);
}