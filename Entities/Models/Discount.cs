namespace Entities.Models;

public partial class Discount
{
    public Guid DiscountId { get; set; }

    public Guid DiscountTypeId { get; set; }

    public decimal DiscountValue { get; set; }

    public string? Description { get; set; }

    public virtual DiscountType DiscountType { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
