using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;
using System.Globalization;

namespace SalesSystem.BLL.Services
{
    public class SaleService: ISaleService
    {
        private readonly IGenericRepository<SaleDetail> _saleDetailRepository;
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public SaleService(IGenericRepository<SaleDetail> saleDetailRepositoryaleRepository,
                           ISaleRepository saleRepository,
                           IMapper mapper)
        {
            _saleDetailRepository = saleDetailRepositoryaleRepository;
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<SaleDTO> Create(SaleDTO SaleDTO)
        {
            try
            {
                var saleCreated = await _saleRepository.Create(_mapper.Map<Sale>(SaleDTO));
                if(saleCreated.IdSale == 0)
                {
                    throw new TaskCanceledException("The Sale could not be created");
                }
                return _mapper.Map<SaleDTO>(saleCreated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<SaleDTO>> History(string searchFor, string saleNumber, string startDate, string endDate)
        {
            IQueryable<Sale> query = await _saleRepository.GetAll();

            var sales = new List<Sale>();

            try
            {
                if (searchFor == "date")
                {
                    DateTime start_Date = DateTime.ParseExact(startDate, "MM/dd/yyyy", new CultureInfo("en-US"));
                    DateTime end_Date = DateTime.ParseExact(endDate, "MM/dd/yyyy", new CultureInfo("en-US"));

                    sales = await query.Where(filter =>
                                        filter.Timestamp.Value.Date >= start_Date &&
                                        filter.Timestamp.Value.Date <= end_Date)
                        .Include(sd => sd.SaleDetails)
                        .ThenInclude(p => p.IdProductNavigation)
                        .ToListAsync();
                }
                else
                {
                    sales = await query.Where(filter =>
                                            filter.Idnumber == saleNumber)
                            .Include(sd => sd.SaleDetails)
                            .ThenInclude(p => p.IdProductNavigation)
                            .ToListAsync();
                }

                return _mapper.Map<List<SaleDTO>>(sales);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<ReportDTO>> Report(string startDate, string endDate)
        {
            IQueryable<SaleDetail> query = await _saleDetailRepository.GetAll();
            var salesDetail = new List<SaleDetail>();

            try
            {
                DateTime start_Date = DateTime.ParseExact(startDate, "MM/dd/yyyy", new CultureInfo("en-US"));
                DateTime end_Date = DateTime.ParseExact(endDate, "MM/dd/yyyy", new CultureInfo("en-US"));

                salesDetail = await query
                          .Include(sd => sd.IdProductNavigation)
                          .Include(p => p.IdSaleNavigation)
                          .Where(sd => sd.IdSaleNavigation.Timestamp.Value.Date >= start_Date
                                       && sd.IdSaleNavigation.Timestamp.Value.Date <= end_Date)
                          .ToListAsync();

                return _mapper.Map<List<ReportDTO>>(salesDetail);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
