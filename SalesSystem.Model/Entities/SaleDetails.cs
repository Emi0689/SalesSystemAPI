﻿using System;
using System.Collections.Generic;

namespace SalesSystem.Model.Entities;

public partial class SaleDetails
{
    public int IdSaleDetails { get; set; }

    public int? IdSale { get; set; }

    public int? IdProduct { get; set; }

    public int? Amount { get; set; }

    public decimal? Price { get; set; }

    public decimal? Total { get; set; }

    public virtual Product? IdProductNavigation { get; set; }

    public virtual Sale? IdSaleNavigation { get; set; }
}
