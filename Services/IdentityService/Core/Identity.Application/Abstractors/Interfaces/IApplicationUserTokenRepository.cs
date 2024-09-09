using Identity.Domain.Entities;

namespace Identity.Application.Abstractors.Interfaces;

public interface IApplicationUserTokenRepository
{
    IQueryable<ApplicationUserToken> GetAllUserTokens();
    
    Task CreateAsync(ApplicationUserToken token);
    
    Task UpdateAsync(ApplicationUserToken token);
    
    Task DeleteAsync(ApplicationUserToken token);
}