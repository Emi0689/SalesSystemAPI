using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;
using System.Globalization;

namespace SalesSystem.BLL.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public DashboardService(IGenericRepository<Product> productRepository, ISaleRepository saleRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<DashboardDTO> Resume()
        {
            DashboardDTO vmDashboard = new DashboardDTO();
            try
            {
                vmDashboard.SalesTotal = await SalesTotalLastWeek();
                vmDashboard.RevenuesTotal = await RevenuesTotalWeek();
                vmDashboard.ProductTotal = await ProductTotal();

                List<WeekSaleDTO> weekSaleDTOs = new List<WeekSaleDTO>();

                foreach(KeyValuePair<string, int> item in SaleLastWeek())
                {
                    weekSaleDTOs.Add(new WeekSaleDTO()
                    {
                        Date = item.Key,
                        Total = item.Value,
                    });
                }
                vmDashboard.weekSaleDTOs = weekSaleDTOs;
                return vmDashboard;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private IQueryable<Sale> SalesReturn(IQueryable<Sale> saleQuery, int days)
        {
            DateTime? lastDate = saleQuery.OrderByDescending(s => s.Timestamp).Select(s=> s.Timestamp).First();

            lastDate = lastDate.Value.AddDays(days);

            return saleQuery.Where(s =>  s.Timestamp.Value.Date >= lastDate.Value.Date);
        }

        private async Task<int> SalesTotalLastWeek()
        {
            int total = 0;
            IQueryable<Sale> _saleQuery = _saleRepository.GetAll();

            if(_saleQuery.Count() > 0)
            {
                var saleTable = SalesReturn(_saleQuery, -7);
                total = await saleTable.CountAsync();
            }
            return total;
        }

        private async Task<string> RevenuesTotalWeek()
        {
            decimal total = 0;
            IQueryable<Sale> _saleQuery = _saleRepository.GetAll();

            if (_saleQuery.Any())
            {
                var saleTable = SalesReturn(_saleQuery, -7);
                total = await saleTable.Select(s => s.Total).SumAsync(v => v.Value);
            }
            return Convert.ToString(total, new CultureInfo("en-US"));
        }

        private async Task<int> ProductTotal()
        {
            IQueryable<Product> _queryProduct = _productRepository.GetAll();
            int total = await _queryProduct.CountAsync();

            return total;
        }

        private Dictionary<string, int> SaleLastWeek()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            IQueryable<Sale> _querySale = _saleRepository.GetAll();

            if (_querySale.Any())
            {
                var saleTable = SalesReturn(_querySale, -7);
                result = saleTable.GroupBy(s => s.Timestamp.Value.Date).OrderBy(x => x.Key)
                    .Select(s => new { date = s.Key.ToString("MM/dd/yyyy"), total = s.Count() })
                    .ToDictionary(keySelector: d => d.date, elementSelector: d => d.total);

            }

            return result;
        }
    }
}
