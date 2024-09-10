namespace Identity.Application.Handlers.Login;

public record LoginResponse(
    string Id,
    string FirstName,
    string LastName,
    string UserName,
    string EmailAddress,
    string JwtToken,
    string RefreshToken
);