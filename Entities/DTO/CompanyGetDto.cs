namespace Entities.DTO;

public partial class CompanyGetDto
{
    public Guid CompanyId { get; set; }

    public string CompanyName { get; set; } = null!;

    public string TaxNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Website { get; set; }

    public string? LogoUrl { get; set; }

    public virtual AddressGetDto Address { get; set; } = null!;

    public virtual StatusGetDto Status { get; set; } = null!;
}

