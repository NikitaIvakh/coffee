using Identity.Domain.Entities;

namespace Identity.Application.Abstractors.Interfaces;

public interface IRoleRepository
{
    Task Create(UserRole userRole);
    
    Task Create(ApplicationUserRole applicationUserRole);
}