namespace Entities.DTO;

public partial class ProductImagePutDto
{
    public Guid ProductImageId { get; set; }

    public Guid ProductId { get; set; }

    public string ImageUrl { get; set; } = null!;
}
