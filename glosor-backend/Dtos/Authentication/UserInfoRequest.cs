namespace glosor_backend.Dtos.Authentication;
public class UserInfoRequest
{
    public required string UserName { get; set; }
    public required string Password { get; set; }
}