namespace Identity.Domain.Shared;

public interface IValidationResult
{
    public static readonly Error ValidationError = new("Validation error", "A validation problem occurred");
    
    public Error[] Errors { get; }
}