namespace Entities.Models;

public partial class Address
{
    public Guid AddressId { get; set; }

    public Guid? CompanyId { get; set; }

    public Guid? UserId { get; set; }

    public Guid AddressTypeId { get; set; }

    public string StreetAddress { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public Guid CountryId { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsDefault { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual AddressType AddressType { get; set; } = null!;

    public virtual ICollection<Company> Companies { get; set; } = new List<Company>();

    public virtual Country Country { get; set; } = null!;
}
