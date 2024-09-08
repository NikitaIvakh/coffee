using Identity.Application.Abstractors.Interfaces;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repository;

public class TokenRepository(ApplicationDbContext context) : ITokenRepository
{
    public IQueryable<ApplicationUserToken> GetAll()
    {
        return context.ApplicationUserTokens.AsNoTracking().AsQueryable();
    }

    public async Task Create(ApplicationUserToken applicationUserToken)
    {
        ArgumentNullException.ThrowIfNull(applicationUserToken);
        await context.AddAsync(applicationUserToken);
    }

    public async Task Update(ApplicationUserToken applicationUserToken)
    {
        ArgumentNullException.ThrowIfNull(applicationUserToken);
        
        var existingToken = await context.ApplicationUserTokens
            .FirstOrDefaultAsync(t => t.ApplicationUserId == applicationUserToken.ApplicationUserId);

        if (existingToken == null)
            await context.AddAsync(applicationUserToken);
        
        else
            context.Entry(existingToken).CurrentValues.SetValues(applicationUserToken);
    }

    public Task Delete(ApplicationUserToken applicationUserToken)
    {
        ArgumentNullException.ThrowIfNull(applicationUserToken);
        context.Remove(applicationUserToken);
        return Task.CompletedTask;
    }
}