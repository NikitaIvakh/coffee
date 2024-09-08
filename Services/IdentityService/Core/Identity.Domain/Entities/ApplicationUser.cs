using System.Text.RegularExpressions;
using Identity.Domain.Common;
using Identity.Domain.Primitives;
using Identity.Domain.Shared;

namespace Identity.Domain.Entities;

public partial class ApplicationUser : Entity, IAuditableData
{
    private ApplicationUser() {}

    private ApplicationUser(Guid id, string firstName, string lastName, string userName, string emailAddress,
        string password, string passwordConfirm) : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        EmailAddress = emailAddress;
        Password = password;
        PasswordConfirm = passwordConfirm;
    }

    public string FirstName { get; private set; }

    public string LastName { get; private set; }

    public string UserName { get; private set; }

    public string EmailAddress { get; private set; }

    public string Password { get; private set; }

    public string PasswordConfirm { get; private set; }

    public DateTimeOffset CreatedAt { get; set; }

    public static ResultT<ApplicationUser> Create(string firstName, string lastName, string userName, string emailAddress, string password, string passwordConfirm)
    {
        const string emailRegex = @"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$";
        
        if (firstName.Length is > Constraints.MaxLength or < Constraints.MinLength)
            return Result.Failure<ApplicationUser>(DomainErrors.ApplicationUser.InvalidLength(nameof(firstName)));

        if (lastName.Length is > Constraints.MaxLength or < Constraints.MinLength)
            return Result.Failure<ApplicationUser>(DomainErrors.ApplicationUser.InvalidLength(nameof(lastName)));
        
        if (userName.Length is > Constraints.MaxLength or < Constraints.MinLength)
            return Result.Failure<ApplicationUser>(DomainErrors.ApplicationUser.InvalidLength(nameof(userName)));

        if (emailAddress.Length is > Constraints.MaxLength or < Constraints.MinLength)
            return Result.Failure<ApplicationUser>(DomainErrors.ApplicationUser.InvalidLength(nameof(userName)));

        if (!MyEmailRegex().IsMatch(emailAddress))
            return Result.Failure<ApplicationUser>(DomainErrors.ApplicationUser.InvalidEmailAddress(nameof(emailAddress)));

        if (password.Length is > Constraints.MaxPasswordLenght or < Constraints.MinPasswordLenght)
            return Result.Failure<ApplicationUser>(DomainErrors.ApplicationUser.InvalidLength(nameof(password)));
        
        if(passwordConfirm.Length is > Constraints.MaxPasswordLenght or < Constraints.MinPasswordLenght)
            return Result.Failure<ApplicationUser>(DomainErrors.ApplicationUser.InvalidLength(nameof(passwordConfirm)));
        
        var user = new ApplicationUser(Guid.NewGuid(), firstName, lastName, userName, emailAddress, password, passwordConfirm);
        return Result.Create(user);
    }

    [GeneratedRegex(@"^[^@\s]+@[^@\s]+\.(com|net|org|gov)$", RegexOptions.IgnoreCase, "en-US")]
    private static partial Regex MyEmailRegex();
}