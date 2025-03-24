namespace Entities.DTO;

public partial class AddressGetDto
{
    public Guid AddressId { get; set; }

    public string StreetAddress { get; set; } = null!;

    public string City { get; set; } = null!;

    public string State { get; set; } = null!;

    public string PostalCode { get; set; } = null!;

    public Guid CountryId { get; set; }

    public string? PhoneNumber { get; set; }

    public bool IsDefault { get; set; }

    public virtual AddressTypeGetDto AddressType { get; set; } = null!;

    public virtual CountryGetDto Country { get; set; } = null!;
}
