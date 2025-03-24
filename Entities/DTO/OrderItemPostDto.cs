namespace Entities.DTO;

public partial class OrderItemPostDto
{
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public double Price { get; set; }

    public string Currency { get; set; } = null!;
}
