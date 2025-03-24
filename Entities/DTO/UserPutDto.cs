namespace Entities.DTO;

public partial class UserPutDto
{
    public Guid UserId { get; set; }

    public Guid CompanyId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public string PasswordHash { get; set; } = null!;

    public Guid RoleId { get; set; }

    public Guid StatusId { get; set; }

    public string? ProfileImageUrl { get; set; }
}
