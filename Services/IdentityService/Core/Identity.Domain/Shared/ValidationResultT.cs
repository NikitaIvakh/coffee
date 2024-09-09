namespace Identity.Domain.Shared;

public class ValidationResultT<TValue>(Error[] error) 
    : ResultT<TValue>(default, false, IValidationResult.ValidationError), IValidationResult
{
    public Error[] Errors { get; } = error;

    public static ValidationResultT<TValue> WithErrors(Error[] errors) => new(errors);
}