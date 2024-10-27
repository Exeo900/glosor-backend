using Core.Entities;
using Core.Ports;
using Core.ValueObjects.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Adapter.Authentication;
public class TokenService : ITokenService
{
    private readonly IConfiguration _configuration;

    public TokenService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public TokenAuthenticationDetails GenerateToken(User user)
    {
        var jwtOptions = new JwtOptions() 
        {
            Issuer = _configuration["JwtSettings:Issuer"]!,
            Audience = _configuration["JwtSettings:Audience"]!, 
            SecretKey = _configuration["JwtSettings:Key"]!
        };

        var claims = new Claim[] 
        { 
            new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new(JwtRegisteredClaimNames.Email, user.UserName) 
        };

        var key = Encoding.UTF8.GetBytes(jwtOptions.SecretKey);

        var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            jwtOptions.Issuer,
            jwtOptions.Audience,
            claims,
            null,
            DateTime.Now.AddHours(1),
            signingCredentials);

        var tokenDetails = new TokenAuthenticationDetails()
        {
            Token = new JwtSecurityTokenHandler().WriteToken(token),
            RefreshTokenId = Guid.NewGuid().ToString()  // TODO: Add refresh token expiration date.
        };

        return tokenDetails;
    }
}
