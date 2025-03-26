namespace Entities.DTO;

public partial class AuthenticateResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Role { get; set; }
    public string Token { get; set; }

    public AuthenticateResponse(UserGetDto user, string token)
    {
        Id = user.UserId;
        FirstName = user.FirstName;
        LastName = user.LastName;
        Role = user.Role.RoleName;
        Token = token;
    }
}
