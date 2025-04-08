using System;
using System.Collections.Generic;

namespace SalesSystem.Model.Entities;

public partial class Sale
{
    public int IdSale { get; set; }

    public string? IdNumber { get; set; }

    public string? PaymentType { get; set; }

    public decimal? Total { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual ICollection<SaleDetails> SaleDetails { get; set; } = new List<SaleDetails>();
}
