﻿using System;
using System.Collections.Generic;

namespace SalesSystem.Model.Entities;

public partial class Sale
{
    public int IdSale { get; set; }

    public string? Idnumber { get; set; }

    public string? PaymentType { get; set; }

    public decimal? Total { get; set; }

    public DateTime? Timestamp { get; set; }

    public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
}
