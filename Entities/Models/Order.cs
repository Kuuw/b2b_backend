namespace Entities.Models;

public partial class Order
{
    public Guid OrderId { get; set; }

    public Guid UserId { get; set; }

    public Guid? DiscountId { get; set; }

    public Guid StatusId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual Discount? Discount { get; set; }

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
