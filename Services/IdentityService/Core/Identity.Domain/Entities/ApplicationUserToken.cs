using Identity.Domain.Primitives;

namespace Identity.Domain.Entities;

public class ApplicationUserToken : Entity
{
    private ApplicationUserToken()
    {
    }

    private ApplicationUserToken(Guid id, Guid applicationUserId, string refreshToken, int refreshTokenExpiresTime) : base(id)
    {
        ApplicationUserId = applicationUserId;
        RefreshToken = refreshToken;
    }

    public ApplicationUser? ApplicationUser { get; private set; }

    public Guid ApplicationUserId { get; private set; }

    public string RefreshToken { get; private set; }
    
    public int RefreshTokenExpiresTime { get; private set; }

    public static ApplicationUserToken Create(Guid applicationUserId, string refreshToken, int refreshTokenExpiresTime)
    {
        return new ApplicationUserToken(Guid.NewGuid(), applicationUserId, refreshToken, refreshTokenExpiresTime);
    }
}