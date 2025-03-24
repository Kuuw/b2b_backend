using Entities.Models;

namespace Entities.DTO;

public partial class PaymentGetDto
{
    public Guid PaymentId { get; set; }

    public DateTime PaymentDate { get; set; }

    public double Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string TransactionReference { get; set; } = null!;

    public virtual InvoiceGetDto Invoice { get; set; } = null!;
}
