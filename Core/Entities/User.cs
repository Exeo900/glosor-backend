namespace Core.Entities;
public class User : Entity
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
    public string? RefreshTokenId { get; set; }
}
