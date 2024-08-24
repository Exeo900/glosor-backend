using Core.Entities;

namespace Core.Ports;

public interface IUserRepository
{
    Task<User?> GetUserByRefreshToken(Guid refreshToken);
    Task<User?> GetUserByUserNameAndPassword(string userName, string password);
    void Update(User user);
}
