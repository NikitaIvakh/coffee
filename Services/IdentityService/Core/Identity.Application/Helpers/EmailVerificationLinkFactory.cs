using Identity.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Identity.Application.Helpers;

public sealed class EmailVerificationLinkFactory(
    IHttpContextAccessor httpContextAccessor,
    LinkGenerator linkGenerator)
{
    public string Create(EmailVerificationToken token)
    {
        var httpContext = httpContextAccessor.HttpContext;
        if (httpContext == null)
        {
            throw new InvalidOperationException("HttpContext is not available.");
        }

        var scheme = httpContext.Request.Scheme;
        var host = new HostString("localhost", 9020); // Ensure port is included

        var verificationLink = linkGenerator.GetUriByAction(
            httpContext,
            action: "VerifyEmailToken",
            controller: "Identity",
            values: new { token = token.Id }, 
            scheme: scheme,
            host: host
        );

        return verificationLink ?? throw new Exception("Could not create email verification link");
    }
}