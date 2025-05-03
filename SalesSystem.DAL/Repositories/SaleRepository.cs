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

                ///////////remove product from stock/////////
                foreach (SaleDetails sl in sale.SaleDetails)
                {
                    var product_found = await _unitOfWork.GetGenRepo<Product>().GetAsync(p => p.IdProduct == sl.IdProduct);
                    if (product_found != null)
                    {
                        product_found.Stock = product_found.Stock - sl.Amount;
                        _unitOfWork.Update(product_found);
                    }
                    else
                    {
                        throw new Exception($"The product number: {sl.IdProduct} could not be found.");
                    }
                }
                await _unitOfWork.CommitAsync();
                /////////////////////////////////////////////

                ///////////new ID value/////////
                var idnumberNext = await _unitOfWork.GetGenRepo<IdNumber>().GetAsync();
                if(idnumberNext != null)
                {
                    idnumberNext.LastNumber = idnumberNext.LastNumber + 1;
                    idnumberNext.Timestamp = DateTime.Now;
                }

                await _unitOfWork.CommitAsync();
                /////////////////////////////////////////////


                int numberOfDigits = 4; //00001
                string zeros = string.Concat(Enumerable.Repeat("0", numberOfDigits));
                string iDSaleNumber = zeros + idnumberNext.LastNumber.ToString();

                iDSaleNumber = iDSaleNumber.Substring(iDSaleNumber.Length - numberOfDigits, numberOfDigits);

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
