using Identity.Domain.Entities;

namespace Identity.Application.Abstractors.Interfaces;

public interface ITokenRepository
{
    IQueryable<ApplicationUserToken> GetAll();
    
    Task Create(ApplicationUserToken applicationUserToken);

    Task Update(ApplicationUserToken applicationUserToken);

    Task Delete(ApplicationUserToken applicationUserToken);
}