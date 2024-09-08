using Identity.Application.Abstractors.Interfaces;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repository;

public class IdentityRepository(ApplicationDbContext context) : IIdentityRepository
{
    public async Task<bool> LoginAsync(string userName, string password)
    {
        var user = await context.ApplicationUsers.FirstOrDefaultAsync(key => key.UserName == userName);
        return user is not null && user.Password == password;
    }

    public async Task RegisterAsync(ApplicationUser user)
    {
        ArgumentNullException.ThrowIfNull(user);
        await context.ApplicationUsers.AddAsync(user);
    }

    public async Task LogoutAsync(Guid id)
    {
        var userToken = await context.ApplicationUserTokens.FirstOrDefaultAsync(key => key.ApplicationUserId == id);

        if (userToken is not null)
            context.ApplicationUserTokens.Remove(userToken);
    }
}