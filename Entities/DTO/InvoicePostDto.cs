﻿namespace Entities.DTO;

public partial class InvoicePostDto
{
    public Guid OrderId { get; set; }

    public Guid CompanyId { get; set; }

    public Guid InvoiceAddressId { get; set; }

    public double TotalAmount { get; set; }

    public double TaxAmount { get; set; }

    public string Currency { get; set; } = null!;

    public Guid StatusId { get; set; }
}
