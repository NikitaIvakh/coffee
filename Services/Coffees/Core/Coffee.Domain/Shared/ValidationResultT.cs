namespace Coffee.Domain.Shared;

public class ValidationResultT<TValue>(Error[] errors)
    : ResultT<TValue>(default, false, IValidationResult.ValidationError), IValidationResult
{
    public Error[] Errors { get; } = errors;

    public static ValidationResultT<TValue> WithErrors(Error[] errors) => new(errors);
}