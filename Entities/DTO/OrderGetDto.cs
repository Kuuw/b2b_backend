using Entities.Models;

namespace Entities.DTO;

public partial class OrderGetDto
{
    public Guid OrderId { get; set; }

    public Guid ShippingAddressId { get; set; }

    public Guid InvoiceAddressId { get; set; }

    public virtual ICollection<InvoiceGetDto> Invoices { get; set; } = new List<InvoiceGetDto>();

    public virtual ICollection<OrderItemGetDto> OrderItems { get; set; } = new List<OrderItemGetDto>();

    public virtual StatusGetDto Status { get; set; } = null!;

    public virtual UserGetDto User { get; set; } = null!;
}
