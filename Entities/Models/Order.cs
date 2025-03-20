namespace Entities.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid UserId { get; set; }

    public Guid StatusId { get; set; }

    public Guid ShippingAddressId { get; set; }

    public Guid InvoiceAddressId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
