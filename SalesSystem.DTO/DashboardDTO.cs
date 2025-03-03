using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalesSystem.DTO
{
    public class DashboardDTO
    {
        public int SalesTotal { get; set; }

        public string? RevenuesTotal { get; set; }

        public int ProductTotal { get; set; }

        public List<WeekSaleDTO> weekSaleDTOs { get; set; }
    }
}
