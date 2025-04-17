namespace Entities.DTO;

public partial class OrderItemPostDto
{
    public Guid ProductId { get; set; }

    public int Quantity { get; set; }
}