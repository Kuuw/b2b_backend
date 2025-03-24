namespace Entities.DTO;

public partial class AddressTypeGetDto
{
    public Guid AddressTypeId { get; set; }

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }
}
