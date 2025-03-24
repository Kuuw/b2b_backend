namespace Entities.DTO;

public partial class OrderItemGetDto
{
    public int Quantity { get; set; }

    public double Price { get; set; }

    public string Currency { get; set; } = null!;

    public virtual ProductGetDto Product { get; set; } = null!;
}
