namespace Entities.Models;

public partial class Payment
{
    public Guid PaymentId { get; set; }

    public Guid InvoiceId { get; set; }

    public string PaymentMethod { get; set; } = null!;

    public DateTime PaymentDate { get; set; }

    public decimal PaymentAmount { get; set; }

    public virtual Invoice Invoice { get; set; } = null!;
}
