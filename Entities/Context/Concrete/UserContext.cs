using Entities.Context.Abstract;

namespace Entities.Context.Concrete;

public class UserContext : IUserContext
{
    public Guid UserId { get; set; }
    public string Role { get; set; } = "";
    public string Email { get; set; } = "";
    public string FirstName { get; set; } = "";
    public string LastName { get; set; } = "";
    public List<string> Permissions { get; set; } = new List<string>();
}
