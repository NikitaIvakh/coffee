using System.Text.RegularExpressions;
using Identity.Domain.Common;
using Identity.Domain.Primitives;
using Identity.Domain.Shared;
using Microsoft.AspNetCore.Identity;

namespace Identity.Domain.Entities;

public partial class ApplicationUser : IdentityUser<string>, IAuditableData
{
    private ApplicationUser()
    {
    }

    private ApplicationUser(string id, string firstName, string lastName, string userName, string? normalizedUserName,
        string? email, string? normalizedEmail, string? passwordHash, string? securityStamp, bool emailConfirmed)
    {
        Id = id;
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        NormalizedUserName = normalizedUserName;
        Email = email;
        NormalizedEmail = normalizedEmail;
        PasswordHash = passwordHash;
        SecurityStamp = securityStamp;
        EmailConfirmed = emailConfirmed;
    }

    public sealed override string Id { get; set; } = string.Empty;

    public string FirstName { get; private set; } = string.Empty;

    public string LastName { get; private set; } = string.Empty;

    public sealed override string? UserName { get; set; } = string.Empty;

    public sealed override string? NormalizedUserName { get; set; } = string.Empty;

    public sealed override string? Email { get; set; } = string.Empty;

    public sealed override string? NormalizedEmail { get; set; } = string.Empty;

    public sealed override string? PasswordHash { get; set; } = string.Empty;

    public sealed override string? SecurityStamp { get; set; } = string.Empty;

    public sealed override bool EmailConfirmed { get; set; }

    public DateTimeOffset CreatedAt { get; set; }

    public static ResultT<ApplicationUser> Create(string id, string firstName, string lastName, string userName,
        string email, string? passwordHash, string? securityStamp, bool emailConfirmed)
    {
        const string emailRegex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";

        if (firstName.Length is > Constraints.MaxLength or < Constraints.MinLength)
            return Result.Failure<ApplicationUser>(DomainErrors.ApplicationUser.InvalidLength(nameof(firstName)));

        if (lastName.Length is > Constraints.MaxLength or < Constraints.MinLength)
            return Result.Failure<ApplicationUser>(DomainErrors.ApplicationUser.InvalidLength(nameof(lastName)));

        if (userName.Length is > Constraints.MaxLength or < Constraints.MinLength)
            return Result.Failure<ApplicationUser>(DomainErrors.ApplicationUser.InvalidLength(nameof(userName)));

        if (email.Length is > Constraints.MaxLength or < Constraints.MinLength)
            return Result.Failure<ApplicationUser>(DomainErrors.ApplicationUser.InvalidLength(nameof(userName)));

        if (!MyEmailRegex().IsMatch(email))
            return Result.Failure<ApplicationUser>(DomainErrors.ApplicationUser.InvalidEmailAddress(nameof(email)));

        var user = new ApplicationUser(id, firstName, lastName, userName, userName.ToUpper(), email, email.ToUpper(), passwordHash, securityStamp, false);
        return Result.Create(user);
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex MyEmailRegex();
}