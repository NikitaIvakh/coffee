using Identity.Domain.Entities;

namespace Identity.Application.Abstractors.Interfaces;

public interface IUserRepository
{
    IQueryable<ApplicationUser> GetAllUsers();
    
    Task<bool> LoginAsync(string userName, string password);
    
    Task RegisterAsync(ApplicationUser user);

    Task LogoutAsync(Guid id);
}