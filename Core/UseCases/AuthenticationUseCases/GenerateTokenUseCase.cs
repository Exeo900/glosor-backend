using Core.Ports;
using Core.Entities;

namespace Core.UseCases.AuthenticationUseCases;

public class GenerateTokenUseCase
{
    private readonly ITokenService _tokenService;

    public GenerateTokenUseCase(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    public string Execute()
    {
        // Todo: Add user table along with a login process and here get user and generate its token, if user name and password match.
        var user = new User() 
        { 
            Id = Guid.NewGuid(),
            Email = "test@test.se", 
            Password = "12341234123412341234" 
        };

        return _tokenService.GenerateToken(user);
    }
}
