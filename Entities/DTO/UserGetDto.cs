namespace Entities.DTO;

public partial class UserGetDto
{
    public Guid UserId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string? ProfileImageUrl { get; set; }

    public virtual CompanyGetDto Company { get; set; } = null!;

    public virtual RoleGetDto Role { get; set; } = null!;

    public virtual StatusGetDto Status { get; set; } = null!;
}
