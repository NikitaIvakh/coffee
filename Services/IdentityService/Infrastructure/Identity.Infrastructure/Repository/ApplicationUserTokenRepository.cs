using Identity.Application.Abstractors.Interfaces;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repository;

public class ApplicationUserTokenRepository(ApplicationDbContext context) : IApplicationUserTokenRepository
{
    public IQueryable<ApplicationUserToken> GetAllUserTokens()
    {
        return context.ApplicationUserTokens.AsNoTracking().AsQueryable();
    }

    public async Task CreateAsync(Domain.Entities.ApplicationUserToken token)
    {
        ArgumentNullException.ThrowIfNull(token);
        await context.ApplicationUserTokens.AddAsync(token);
    }

    public Task UpdateAsync(Domain.Entities.ApplicationUserToken token)
    {
        ArgumentNullException.ThrowIfNull(token);
        context.ApplicationUserTokens.Update(token);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(Domain.Entities.ApplicationUserToken token)
    {
        ArgumentNullException.ThrowIfNull(token);
        context.ApplicationUserTokens.Remove(token);
        return Task.CompletedTask;
    }
}