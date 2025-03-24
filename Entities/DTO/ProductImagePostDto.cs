using Entities.Models;

namespace Entities.DTO;

public partial class ProductImagePostDto
{
    public Guid ProductId { get; set; }

    public string ImageUrl { get; set; } = null!;
}
