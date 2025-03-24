namespace Entities.DTO;

public partial class ProductImageGetDto
{
    public Guid ProductImageId { get; set; }

    public string ImageUrl { get; set; } = null!;
}
