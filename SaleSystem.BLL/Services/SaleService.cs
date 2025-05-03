using AutoMapper;
using Microsoft.EntityFrameworkCore;
using SalesSystem.BLL.Services.Interfaces;
using SalesSystem.DAL.Repositories;
using SalesSystem.DAL.Repositories.Interfaces;
using SalesSystem.DTO;
using SalesSystem.Model.Entities;
using System.Globalization;

namespace SalesSystem.BLL.Services
{
    public class SaleService : ISaleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        internal IGenericRepository<SaleDetails> _productGenSaleDetails;
        internal ISaleRepository _saleRepository;

        public SaleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            this._productGenSaleDetails = _unitOfWork.GetGenRepo<SaleDetails>();
            this._saleRepository = _unitOfWork.SaleRepository;
        }

        public async Task<SaleDTO> CreateAsync(SaleDTO SaleDTO)
        {
            try
            {
                var saleCreated = await _saleRepository.CreateAsync(_mapper.Map<Sale>(SaleDTO));
                if (saleCreated.IdSale == 0)
                {
                    throw new TaskCanceledException("The Sale could not be created.");
                }
                return _mapper.Map<SaleDTO>(saleCreated);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<List<SaleDTO>> HistoryAsync(string searchFor, string saleNumber, string startDate, string endDate)
        {
            IQueryable<Sale> query = _saleRepository.GetAllAsync();

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
                                            filter.IdNumber == saleNumber)
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

        public async Task<List<ReportDTO>> GetReportAsync(string startDate, string endDate)
        {
            IQueryable<SaleDetails> query = _productGenSaleDetails.GetAllAsync();
            var salesDetail = new List<SaleDetails>();

            try
            {
                DateTime start_Date = DateTime.ParseExact(startDate, "MM/dd/yyyy", new CultureInfo("en-US"));
                DateTime end_Date = DateTime.ParseExact(endDate, "MM/dd/yyyy", new CultureInfo("en-US"));

                salesDetail = await query
                          .Include(sd => sd.IdProductNavigation)
                          .Include(p => p.IdSaleNavigation)
                          .Where(t => t.IdSaleNavigation.Timestamp.Value.Date >= start_Date
                                       && t.IdSaleNavigation.Timestamp.Value.Date <= end_Date)
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
