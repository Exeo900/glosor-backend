using Core.Entities.Exceptions;
using Core.Ports;
using Core.ValueObjects.Authentication;
using Serilog;

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

    public async Task<TokenAuthenticationDetails> Execute(string expiredToken, Guid refreshTokenId)
    {
        Log.Information($"Execute {nameof(RefreshTokenUseCase)} - Attempting to refresh JWT token with refresh token id: {{refreshToken}}", refreshTokenId);

        // TODO: decode expiredToken and check if user match with refresh token.

        var user = await _userRepository.GetUserByRefreshToken(refreshTokenId);

        if (user == null)
        {
            // TODO: fix this exception, dont show why user is logged out.
            Log.Information($"Execute {nameof(RefreshTokenUseCase)} - Failed to refresh JWT token with refresh token id: {{refreshToken}}", refreshTokenId);

            throw new UserLoggedOutException($"No user found with refresh token: {refreshTokenId}");
        }

        var tokenDetails = _tokenService.GenerateToken(user);

        user.RefreshTokenId = tokenDetails.RefreshTokenId;

        _userRepository.Update(user);

        Log.Information("Execute RefreshTokenUseCase - Refresh successful! Token details: {@TokenDetails}", tokenDetails);

        return tokenDetails;
    }
}
