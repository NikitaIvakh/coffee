using Identity.Domain.Shared;

namespace Identity.Domain.Entities;

public class ApplicationUserToken
{
    private ApplicationUserToken()
    {
    }

    private ApplicationUserToken(Guid id, string userId, string refreshToken, DateTime refreshTokenExpiryDate)
    {
        Id = id;
        UserId = userId;
        RefreshToken = refreshToken;
        RefreshTokenExpiryDate = refreshTokenExpiryDate;
    }

    public Guid Id { get; private set; }
    
    public string UserId { get; private set; } = string.Empty;
    
    public string RefreshToken { get; private set; }= string.Empty;
    
    public DateTime RefreshTokenExpiryDate { get; private set; }
    
    public virtual ApplicationUser? User { get; private set; }
    
    public static ResultT<ApplicationUserToken> Create(string userId, string refreshToken, DateTime refreshTokenExpiryDate)
    {
        var applicationUserToken = new ApplicationUserToken(Guid.NewGuid(), userId, refreshToken, refreshTokenExpiryDate);
        return Result.Create(applicationUserToken);
    }
}