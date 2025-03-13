namespace Entities.Models;

public partial class AddressType
{
    public Guid AddressTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public virtual ICollection<Address> Addresses { get; set; } = new List<Address>();
}
