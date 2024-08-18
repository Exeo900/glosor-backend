using Core.UseCases.AuthenticationUseCases;
using Core.ValueObjects.Authentication;
using glosor_backend.Dtos.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace glosor_backend.API;

public static class AutheticationEndpoints
{
    [AllowAnonymous]
    public static async Task<TokenAuthenticationDetails> Login([FromBody] UserInfoRequest userInfo, GenerateTokenUseCase generateTokenUseCase)
    {
        var generatedToken = await generateTokenUseCase.Execute(userInfo.UserName, userInfo.Password);

        return generatedToken;
    }

    [AllowAnonymous]
    public static async Task<TokenAuthenticationDetails> Refresh([FromBody] RefreshTokenRequest refreshTokenRequest, RefreshTokenUseCase refreshTokenUseCase)
    {
        var refreshedToken = await refreshTokenUseCase.Execute(refreshTokenRequest.Token, refreshTokenRequest.RefreshTokenId);

        return refreshedToken;
    }
}
