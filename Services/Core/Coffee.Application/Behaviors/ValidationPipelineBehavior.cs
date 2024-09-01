using Coffee.Domain.Shared;
using FluentValidation;
using MediatR;

namespace Coffee.Application.Behaviors;

public sealed class ValidationPipelineBehavior<TRequest, TResponse> (IEnumerable<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse> 
    where TRequest : IRequest<TResponse>
    where TResponse: Result
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        try
        {
            if (!validators.Any())
                return await next();

            var errors = validators
                .Select(validator => validator.Validate(request))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(validationFailure => validationFailure is not null)
                .Select(failure => new Error
                    (
                        code: failure.PropertyName,
                        failure.ErrorMessage
                    )
                )
                .Distinct()
                .ToArray();

            if (errors.Length != 0)
                return CreateValidationResult<TResponse>(errors);

            return await next();
        }
        
        catch (Exception exception)
        {
            throw new Exception(exception.Message);
        }
    }

    private static TResult CreateValidationResult<TResult>(Error[] errors) 
        where TResult : Result
    {
        if (typeof(TRequest) == typeof(Result))
            return (ValidationResult.WithErrors(errors) as TResult)!;

        var validationResult = typeof(ValidationResultT<>)
            .GetGenericTypeDefinition()
            .MakeGenericType(typeof(TResult).GenericTypeArguments[0])
            .GetMethod(nameof(ValidationResult.WithErrors))!
            .Invoke(null, [errors])!;

        return (TResult)validationResult;
    }
}