using Identity.Application.Abstractors.Interfaces;
using Identity.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Identity.Infrastructure.Repository;

public class EmailVerificationTokenRepository(ApplicationDbContext context) : IEmailVerificationTokenRepository
{
    public IQueryable<EmailVerificationToken> GetAll()
    {
        return context.EmailVerificationTokens.AsNoTracking().AsQueryable();
    }

    public async Task Create(EmailVerificationToken emailVerificationToken)
    {
        ArgumentNullException.ThrowIfNull(emailVerificationToken);
        await context.EmailVerificationTokens.AddAsync(emailVerificationToken);
    }

    public Task Update(EmailVerificationToken emailVerificationToken)
    {
        ArgumentNullException.ThrowIfNull(emailVerificationToken);
        context.EmailVerificationTokens.Update(emailVerificationToken);
        return Task.CompletedTask;
    }

    public Task Remove(EmailVerificationToken emailVerificationToken)
    {
        ArgumentNullException.ThrowIfNull(emailVerificationToken);

        var existingEntity = context.ChangeTracker.Entries<ApplicationUser>()
            .FirstOrDefault(e => e.Entity.Id == emailVerificationToken.User.Id);

        if (existingEntity != null)
            context.Entry(existingEntity.Entity).State = EntityState.Detached;

        else
            context.EmailVerificationTokens.Attach(emailVerificationToken);

        context.EmailVerificationTokens.Remove(emailVerificationToken);
        return Task.CompletedTask;
    }
}