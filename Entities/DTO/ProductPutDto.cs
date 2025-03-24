using Entities.Models;

namespace Entities.DTO;

public partial class ProductPutDto
{
    public Guid ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string ProductCode { get; set; } = null!;

    public string? Description { get; set; }

    public Guid CategoryId { get; set; }

    public double Price { get; set; }

    public string Currency { get; set; } = null!;

    public Guid StatusId { get; set; }

    public int MinOrderQuantity { get; set; }
}
