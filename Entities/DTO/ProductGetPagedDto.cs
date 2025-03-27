namespace Entities.DTO;

public partial class ProductGetPagedDto
{
    public int Page { get; set; }
    public int PageSize { get; set; }
    public ProductFilter Filter { get; set; } = new ProductFilter();
}
