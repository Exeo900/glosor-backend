using Core.Entities;
using Core.ValueObjects.Authentication;

namespace Core.Ports;
public interface ITokenService
{
    TokenAuthenticationDetails GenerateToken(User user);
}
