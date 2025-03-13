namespace Entities.Models;

public partial class ProductStock
{
    public Guid ProductStockId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public DateTime LastUpdated { get; set; }

    public virtual Product Product { get; set; } = null!;
}
