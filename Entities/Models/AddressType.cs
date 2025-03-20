namespace Entities.Models;

public partial class AddressType
{
    public Guid AddressTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
