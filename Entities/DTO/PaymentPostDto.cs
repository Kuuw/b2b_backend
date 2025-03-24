namespace Entities.DTO;

public partial class PaymentPostDto
{
    public Guid InvoiceId { get; set; }

    public DateTime PaymentDate { get; set; }

    public double Amount { get; set; }

    public string Currency { get; set; } = null!;

    public string TransactionReference { get; set; } = null!;
}
