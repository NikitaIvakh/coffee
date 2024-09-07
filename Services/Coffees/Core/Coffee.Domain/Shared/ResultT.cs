namespace Coffee.Domain.Shared;

public class ResultT<TValue>(TValue? value, bool isSuccess, Error error) : Result(isSuccess, error)
{
    public TValue Value => IsSuccess ? value! : throw new InvalidOperationException("The value of a failure result can not be accessed.");
}