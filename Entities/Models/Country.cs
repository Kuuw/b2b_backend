namespace Entities.Models;

public partial class Country
{
    public Guid CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public int PhoneCode { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
