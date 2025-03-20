namespace Entities.Models;

public partial class Payment
{
    public Guid PaymentId { get; set; }

    public Guid InvoiceId { get; set; }

    public DateTime PaymentDate { get; set; }

    public double Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string TransactionReference { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
