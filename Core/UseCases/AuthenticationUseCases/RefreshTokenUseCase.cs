using Core.Ports;
using Core.ValueObjects.Authentication;

namespace Core.UseCases.AuthenticationUseCases;

public class RefreshTokenUseCase
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public RefreshTokenUseCase(ITokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<TokenAuthenticationDetails> Execute(string expiredToken, Guid refreshToken)
    {
        var user = await _userRepository.GetUserByRefreshToken(refreshToken);

        // TODO: decode expiredToken and check if user match with refresh token.

        if (user == null)
        {
            throw new Exception("No such refresh token!");
        }

        var token = _tokenService.GenerateToken(user);

        return token;
    }
}
