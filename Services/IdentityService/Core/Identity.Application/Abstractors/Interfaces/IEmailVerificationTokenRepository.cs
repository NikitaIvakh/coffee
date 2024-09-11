using Identity.Domain.Entities;

namespace Identity.Application.Abstractors.Interfaces;

public interface IEmailVerificationTokenRepository
{
    IQueryable<EmailVerificationToken> GetAll();

    Task Create(EmailVerificationToken emailVerificationToken);

    Task Update(EmailVerificationToken emailVerificationToken);
    
    Task Remove(EmailVerificationToken emailVerificationToken);
}