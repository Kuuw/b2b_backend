namespace Entities.DTO;

public partial class CompanyPutDto
{
    public Guid CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string TaxNumber { get; set; } = null!;

    public Guid AddressId { get; set; }

    public string Email { get; set; } = null!;

    public string? Website { get; set; }
}
