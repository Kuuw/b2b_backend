namespace Entities.Models;

public partial class Invoice
{
    public Guid InvoiceId { get; set; }

    public Guid OrderId { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public DateTime InvoiceDate { get; set; }

    public DateTime? DueDate { get; set; }

    public decimal TotalAmount { get; set; }

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
