using Core.Entities;

namespace Core.Ports;
public interface ITokenService
{
    string GenerateToken(User user);
}
