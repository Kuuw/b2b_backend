namespace Entities.DTO;

public partial class ProductPagedResponse: GenericPagedResponse<ProductGetDto>
{
    public double MaxPrice { get; set; }
    public int MaxStock { get; set; }
}
