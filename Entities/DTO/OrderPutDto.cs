namespace Entities.DTO;

public partial class OrderPutDto
{
    public Guid OrderId { get; set; }

    public Guid UserId { get; set; }

    public Guid StatusId { get; set; }

    public Guid ShippingAddressId { get; set; }

    public Guid InvoiceAddressId { get; set; }

    public virtual ICollection<OrderItemPostDto> OrderItems { get; set; } = new List<OrderItemPostDto>();
}
