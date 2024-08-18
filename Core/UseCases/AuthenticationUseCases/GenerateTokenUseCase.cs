using Core.Ports;
using Core.ValueObjects.Authentication;

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
        var user = await _userRepository.GetUserByUserNameAndPassword(userName, password);

        if (user == null)
        {
            throw new Exception("Gick inte att logga in!");
        }

        var tokenDetails =  _tokenService.GenerateToken(user);

        user.RefreshTokenId = tokenDetails.RefreshTokenId;

        _userRepository.Update(user);

        return tokenDetails;
    }
}
