namespace Entities.DTO;

public partial class OrderPostDto
{
    public Guid ShippingAddressId { get; set; }

    public Guid InvoiceAddressId { get; set; }

    public virtual ICollection<OrderItemPostDto> OrderItems { get; set; } = new List<OrderItemPostDto>();
}
