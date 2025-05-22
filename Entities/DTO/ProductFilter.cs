namespace Entities.DTO;

public partial class ProductFilter
{
    public string? ProductName { get; set; }
    public Guid? CategoryId { get; set; }
    public double? MinPrice { get; set; }
    public double? MaxPrice { get; set; }
    public int? MinStock { get; set; }
    public int? MaxStock { get; set; }

    public ProductFilter()
    {
        CategoryId = null;
        MinPrice = null;
        MaxPrice = null;
        MinStock = null;
        MaxStock = null;
    }
}
