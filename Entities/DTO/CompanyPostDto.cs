﻿using Entities.Models;

namespace Entities.DTO;

public partial class CompanyPostDto
{
    public string CompanyName { get; set; } = null!;

    public string TaxNumber { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Website { get; set; }

    public virtual Address Address { get; set; } = null!;
}

