namespace Entities.DTO;

public partial class DiscountPutDto
{
    public Guid DiscountId { get; set; }

    public Guid DiscountTypeId { get; set; }

    public decimal DiscountValue { get; set; }

    public string? Description { get; set; }
}
