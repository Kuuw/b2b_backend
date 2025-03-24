using Entities.Models;

namespace Entities.DTO;

public partial class ProductStockPutDto
{
    public Guid ProductId { get; set; }

    public int StockQuantity { get; set; }
}
