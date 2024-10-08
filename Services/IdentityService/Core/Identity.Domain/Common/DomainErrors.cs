using Identity.Domain.Shared;

namespace Identity.Domain.Common;

public static class DomainErrors
{
    public static class ErrorException
    {
        public static readonly Func<string, Error> Errors = (property) =>
            new Error("exception.error", $"{property}");
    }
    
    public static class ApplicationUser
    {
        public static readonly Func<string, Error> AlreadyExists = (userName) =>
            new Error("user.already.exists", $"{userName} is already exists");

        public static readonly Func<string, Error> UserCanNotRegister = (error) =>
            new Error("user.can.not.register", $"{error}");

        public static readonly Func<string, Error> PasswordsIsNotConfirmed = (property) =>
            new Error("passwords.is.not.confirmed", $"{property}: Passwords is not confirmed");

        public static readonly Func<string, Error> UserNotFound = (property) =>
            new Error("user.not.found", $"user {property} not found");

        public static readonly Func<string, Error> InvalidLength = (property) =>
            new Error("invalid.length", $"{property}: length is invalid");

        public static readonly Func<string, Error> InvalidEmailAddress = (property) =>
            new Error("email.address.is.invalid", $"{property} is invalid email address");
        
        public static readonly Func<string, Error> InvalidPassword = (property) => 
            new Error("password.is.invalid", $"{property} is invalid password");
    }

    public static class ApplicationUserToken
    {
        public static readonly Func<string, Error> InvalidToken = (property) =>
            new Error("invalid.length", $"{property}");
        
        public static readonly Func<string, Error> TokenNotFound = (property) =>
            new Error("token.not.found", $"token with userId: {property} not found");
    }

    public static class UserRole
    {
        public static readonly Func<string, Error> RoleCanNotCreate = (property) =>
            new Error("role.can.not.create", $"{property} role can not create");
    }
}