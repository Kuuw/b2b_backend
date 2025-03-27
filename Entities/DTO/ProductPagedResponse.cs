namespace Entities.DTO;

public partial class ProductPagedResponse
{
    public List<ProductGetDto> Items { get; set; }
    public PageMetadata Metadata { get; set; }
}
