namespace glosor_backend.Dtos.Authentication;
public class RefreshTokenRequest
{
    public required string Token { get; set; }
    public required Guid RefreshTokenId { get; set; }
}