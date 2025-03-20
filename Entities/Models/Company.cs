namespace Entities.Models;

public partial class Company
{
    public Guid CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string TaxNumber { get; set; } = null!;

    public Guid AddressId { get; set; }

    public string Email { get; set; } = null!;

    public string? Website { get; set; }

    public Guid StatusId { get; set; }

    public string? LogoUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual Status Status { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
