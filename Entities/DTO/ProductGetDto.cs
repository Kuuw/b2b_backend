namespace Entities.DTO;

public partial class ProductGetDto
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductCode { get; set; } = null!;

    public string? Description { get; set; }

    public double Price { get; set; }

    public string Currency { get; set; } = null!;

    public int MinOrderQuantity { get; set; }

    public virtual CategoryGetDto Category { get; set; } = null!;

    public virtual ICollection<ProductImageGetDto> ProductImages { get; set; } = new List<ProductImageGetDto>();

    public virtual ProductStockGetDto? ProductStock { get; set; }

    public virtual StatusGetDto Status { get; set; } = null!;
}
