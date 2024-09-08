using Identity.Domain.Common;
using Identity.Domain.Primitives;
using Identity.Domain.Shared;

namespace Identity.Domain.Entities;

public class ApplicationUserToken : Entity
{
    private ApplicationUserToken()
    {
    }

    private ApplicationUserToken(Guid id, Guid applicationUserId, string refreshToken, DateTime refreshTokenExpiresTime) : base(id)
    {
        ApplicationUserId = applicationUserId;
        RefreshToken = refreshToken;
    }

    public ApplicationUser? ApplicationUser { get; private set; }

    public Guid ApplicationUserId { get; private set; }

    public string RefreshToken { get; private set; }
    
    public DateTime RefreshTokenExpiresTime { get; private set; }

    public static ResultT<ApplicationUserToken> Create(Guid applicationUserId, string refreshToken, DateTime refreshTokenExpiresTime)
    {
        if (refreshToken.Length is > Constraints.MaxRefreshTokenMaxLength or < Constraints.MinRefreshTokenMaxLength)
            return Result.Failure<ApplicationUserToken>(DomainErrors.ApplicationUserToken.InvalidLength(nameof(refreshToken)));

        var applicationUserToken = new ApplicationUserToken(Guid.NewGuid(), applicationUserId, refreshToken, refreshTokenExpiresTime);
        return Result.Create(applicationUserToken);
    }
}