﻿namespace Entities.Models;

public partial class Status
{
    public Guid StatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
