using Entities.Models;

namespace Entities.DTO;

public partial class InvoiceGetDto
{
    public Guid InvoiceId { get; set; }

    public double TotalAmount { get; set; }

    public double TaxAmount { get; set; }

    public string Currency { get; set; } = null!;

    public Guid StatusId { get; set; }

    public virtual CompanyGetDto Company { get; set; } = null!;

    public virtual OrderGetDto Order { get; set; } = null!;

    public virtual ICollection<PaymentGetDto> Payments { get; set; } = new List<PaymentGetDto>();
}
