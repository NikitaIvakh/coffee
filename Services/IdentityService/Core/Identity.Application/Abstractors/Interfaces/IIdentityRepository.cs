using Identity.Domain.Entities;

namespace Identity.Application.Abstractors.Interfaces;

public interface IIdentityRepository
{
    Task<bool> LoginAsync(string userName, string password);
    
    Task RegisterAsync(ApplicationUser user);

    Task LogoutAsync(Guid id);
}