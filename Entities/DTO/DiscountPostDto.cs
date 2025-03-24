namespace Entities.DTO;

public partial class DiscountPostDto
{
    public Guid DiscountTypeId { get; set; }

    public decimal DiscountValue { get; set; }

    public string? Description { get; set; }
}
