using Identity.Domain.Shared;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Identity.WebAPI.Abstractors;

public abstract class ApiController(ISender sender) : ControllerBase
{
    protected ActionResult<ResultT<T>> HandleFailure<T>(Result result)
    {
        return result switch
        {
            { IsSuccess: true } => throw new InvalidOperationException(),
            IValidationResult validationResult =>
                BadRequest(CreateProblemDetails(
                    "Validation failure",
                    StatusCodes.Status400BadRequest,
                    result.Error,
                    validationResult.Errors
                )),
            _ => BadRequest(CreateProblemDetails("Bad Request", StatusCodes.Status400BadRequest, result.Error))
        };
    }

    private static ProblemDetails CreateProblemDetails(string title, int status, Error error, Error[]? errors = null)
    {
        return new ProblemDetails()
        {
            Title = title,
            Type = error.Code,
            Detail = error.Message,
            Status = status,
            Extensions = { { nameof(errors), errors } }
        };
    }
}