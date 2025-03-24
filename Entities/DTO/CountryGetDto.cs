namespace Entities.DTO;

public partial class CountryGetDto
{
    public Guid CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public int PhoneCode { get; set; }
}
