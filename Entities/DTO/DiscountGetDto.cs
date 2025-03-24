namespace Entities.DTO;

public partial class DiscountGetDto
{
    public Guid DiscountId { get; set; }

    public decimal DiscountValue { get; set; }

    public string? Description { get; set; }

    public virtual DiscountTypeGetDto DiscountType { get; set; } = null!;
}
