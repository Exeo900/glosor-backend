using Core.Ports;
using Core.ValueObjects.Authentication;
using Serilog;

namespace Core.UseCases.AuthenticationUseCases;

public class GenerateTokenUseCase
{
    private readonly ITokenService _tokenService;
    private readonly IUserRepository _userRepository;

    public GenerateTokenUseCase(ITokenService tokenService, IUserRepository userRepository)
    {
        _tokenService = tokenService;
        _userRepository = userRepository;
    }

    public async Task<TokenAuthenticationDetails> Execute(string userName, string password)
    {
        Log.Information($"Execute {nameof(GenerateTokenUseCase)} - Attempting to log in with user credentials: {{userName}} {{password}}", userName, password);

        var user = await _userRepository.GetUserByUserNameAndPassword(userName, password);

        if (user == null)
        {
            Log.Information($"Execute {nameof(GenerateTokenUseCase)} - Login failed with user credentials: {{userName}} {{password}}", userName, password);

            throw new Exception("Gick inte att logga in!");
        }

        var tokenDetails =  _tokenService.GenerateToken(user);

        user.RefreshTokenId = tokenDetails.RefreshTokenId;

        _userRepository.Update(user);

        Log.Information("Execute GenerateTokenUseCase - Login and token creation was successful! Token details: {@TokenDetails}",  tokenDetails);

        return tokenDetails;
    }
}
