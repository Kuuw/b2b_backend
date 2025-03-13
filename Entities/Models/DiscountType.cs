namespace Entities.Models;

public partial class DiscountType
{
    public Guid DiscountTypeId { get; set; }

    public string DiscountTypeName { get; set; } = null!;

    public virtual ICollection<Discount> Discounts { get; set; } = new List<Discount>();
}
