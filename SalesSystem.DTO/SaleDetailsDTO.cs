using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.DTO
{
    public class SaleDetailsDTO
    {
        public int? IdProduct { get; set; }

        public string? ProductDescription { get; set; }

        public int? Amount { get; set; }

        public string? PriceText { get; set; }

        public string? TotalText { get; set; }
    }
}
