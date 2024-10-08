using Identity.Domain.Entities;

namespace Identity.Application.Service;

public interface ITokenService
{
    string CreateToken(ApplicationUser user, IEnumerable<string> roles);
}