namespace Entities.Models;

public partial class Address
{
    public Guid AddressId { get; set; }

    public Guid AddressTypeId { get; set; }

    public Guid CountryId { get; set; }

    public Guid? UserId { get; set; }

    public string Street { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public virtual AddressType AddressType { get; set; } = null!;

    public virtual Country Country { get; set; } = null!;

    public virtual User? User { get; set; }
}
