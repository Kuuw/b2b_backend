namespace Entities.Models;

public partial class ProductImage
{
    public Guid ProductImageId { get; set; }

    public Guid ProductId { get; set; }

    public string ImageUrl { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
