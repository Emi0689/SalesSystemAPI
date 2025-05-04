using SalesSystem.Model.Entities;
using SalesSystem.DAL.Repositories.Interfaces;

namespace SalesSystem.DAL.Repositories
{
    public class SaleRepository: GenericRepository<Sale>, ISaleRepository
    {
        private readonly IUnitOfWork _unitOfWork;

        public SaleRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Sale> CreateSaleAsync(Sale sale)
        {
            Sale saleGenerated = new Sale();

            _unitOfWork.BeginTransaction();

            try
            {
                // 1. Stock
                foreach (SaleDetails sl in sale.SaleDetails)
                {
                    var product_found = await _unitOfWork
                        .GetGenRepo<Product>()
                        .GetAsync(p => p.IdProduct == sl.IdProduct);

                    if (product_found is null)
                        throw new Exception($"The product number: {sl.IdProduct} could not be found.");

                    product_found.Stock -= sl.Amount;
                    _unitOfWork.Update(product_found);
                }

                await _unitOfWork.CommitAsync();

                // 2. New sale number
                var idnumberNext = await _unitOfWork
                    .GetGenRepo<IdNumber>()
                    .GetAsync();

                if (idnumberNext is not null)
                {
                    idnumberNext.LastNumber += 1;
                    idnumberNext.Timestamp = DateTime.Now;
                }

                await _unitOfWork.CommitAsync();

                // 3. Get Sale ID
                int numberOfDigits = 4; //00001
                string iDSaleNumber = (new string('0', numberOfDigits) + idnumberNext.LastNumber)
                                      .Substring(idnumberNext.LastNumber.ToString().Length);

                sale.IdNumber = iDSaleNumber;

                await _unitOfWork.AddAsync(sale);
                await _unitOfWork.CommitAsync();

                saleGenerated = sale;

                _unitOfWork.CommitTransaction();
            }
            catch
            {
                _unitOfWork.RollbackTransaction();
                throw;
            }

            return saleGenerated;
        }
    }
}
