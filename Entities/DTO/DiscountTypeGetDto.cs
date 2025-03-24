namespace Entities.DTO;

public partial class DiscountTypeGetDto
{
    public Guid DiscountTypeId { get; set; }

    public string DiscountTypeName { get; set; } = null!;
}
