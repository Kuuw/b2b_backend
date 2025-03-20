namespace Entities.Models;

public partial class ProductStock
{
    public Guid ProductId { get; set; }

    public int StockQuantity { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
