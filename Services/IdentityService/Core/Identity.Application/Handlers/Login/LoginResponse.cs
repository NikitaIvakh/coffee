namespace Identity.Application.Handlers.Login;

public record LoginResponse(string AccessToken, string RefreshToken, string UserName, string EmailAddress);