namespace Entities.DTO;

public partial class ProductStockGetDto
{
    public Guid ProductId { get; set; }

    public int StockQuantity { get; set; }
}
