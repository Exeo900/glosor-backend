namespace Core.ValueObjects.Authentication;
public class TokenAuthenticationDetails
{
    public required string Token { get; set; }
    public required string RefreshTokenId { get; set; }
}
