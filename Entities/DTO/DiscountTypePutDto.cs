namespace Entities.DTO;

public partial class DiscountTypePutDto
{
    public Guid DiscountTypeId { get; set; }

    public string DiscountTypeName { get; set; } = null!;
}
