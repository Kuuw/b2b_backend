namespace Entities.Models;

public partial class Product
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

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Category Category { get; set; } = null!;

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual ICollection<ProductImage> ProductImages { get; set; } = new List<ProductImage>();

    public virtual ProductStock? ProductStock { get; set; }

    public virtual Status Status { get; set; } = null!;
}
