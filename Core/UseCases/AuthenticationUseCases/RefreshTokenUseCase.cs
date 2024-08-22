using Core.Entities.Exceptions;
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
        // TODO: decode expiredToken and check if user match with refresh token.

        var user = await _userRepository.GetUserByRefreshToken(refreshToken);

        if (user == null)
        {
            // TODO: fix this exception, dont show why user is logged out.
            throw new UserLoggedOutException($"No user found with refresh token: {refreshToken}");
        }

        var token = _tokenService.GenerateToken(user);

        user.RefreshTokenId = token.RefreshTokenId;

        _userRepository.Update(user);

        return token;
    }
}
