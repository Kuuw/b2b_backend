namespace Entities.Models;

public partial class Invoice
{
    public Guid InvoiceId { get; set; }

    public Guid OrderId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid InvoiceAddressId { get; set; }

    public double TotalAmount { get; set; }

    public double TaxAmount { get; set; }

    public string Currency { get; set; } = null!;

    public Guid StatusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Company Company { get; set; } = null!;

    public virtual Order Order { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();
}
