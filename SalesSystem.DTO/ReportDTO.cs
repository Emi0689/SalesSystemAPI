using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.DTO
{
    public class ReportDTO
    {
        public string? IdNumber { get; set; }

        public string? PaymentType { get; set; }

        public string? Timestamp { get; set; }

        public string? TotalSale { get; set; }

        public string? Product { get; set; }

        public int? Amount { get; set; }

        public string? Price { get; set; }

        public string? Total { get; set; }
    }
}
