using Core.Entities;
using Core.Ports;
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

    public string GenerateToken(User user)
    {
        var jwtOptions = new JwtOptions() 
        {
            Issuer = _configuration["JwtSettings:Issuer"]!,
            Audience = _configuration["JwtSettings:Audience"]!, 
            SecretKey = _configuration["JwtSettings:Key"]!
        };

        var claims = new Claim[] 
        { 
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email)        
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

        string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);

        return tokenValue;
    }
}
