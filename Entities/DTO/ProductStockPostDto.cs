using Entities.Models;

namespace Entities.DTO;

public partial class ProductStockPostDto
{
    public Guid ProductId { get; set; }

    public int StockQuantity { get; set; }
}
