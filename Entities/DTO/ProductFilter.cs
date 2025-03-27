namespace Entities.DTO;

public partial class ProductFilter
{
    public string? ProductName { get; set; }
    public Guid? CategoryId { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }

    public ProductFilter()
    {
        CategoryId = null;
        MinPrice = null;
        MaxPrice = null;
    }
}
