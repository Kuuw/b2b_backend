namespace Entities.Models;

public partial class User
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

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public DateTime? LastLoginAt { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual ICollection<Log> Logs { get; set; } = new List<Log>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual Role Role { get; set; } = null!;

    public virtual Status Status { get; set; } = null!;
}
