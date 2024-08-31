namespace Coffee.Domain.Shared;

public class Result
{
    protected Result(bool isSuccess, Error error)
    {
        switch (isSuccess)
        {
            case true when error != Error.None:
                throw new InvalidOperationException("Success result should not have an error.");
            case false when error == Error.None:
                throw new InvalidOperationException("Failure result should have an error.");
            default:
                IsSuccess = isSuccess;
                Error = error;
                break;
        }
    }
    
    public bool IsSuccess { get; }

    public bool IsFailure => !IsSuccess;
    
    public Error Error { get; }

    public static Result Success() => new(true, Error.None);

    public static ResultT<TValue> Success<TValue>(TValue value) => new(value, true, Error.None);

    public static Result Failure(Error error) => new(false, error);

    public static ResultT<TValue> Failure<TValue>(Error error) => new(default, false, error);

    public static ResultT<TValue> Create<TValue>(TValue? value) => value is not null ? Success(value) : Failure<TValue>(Error.NullValue);
}