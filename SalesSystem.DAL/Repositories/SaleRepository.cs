using SalesSystem.DAL.DBContext;
using SalesSystem.Model.Entities;
using SalesSystem.DAL.Repositories.Interfaces;

namespace SalesSystem.DAL.Repositories
{
    public class SaleRepository: GenericRepository<Sale>, ISaleRepository
    {
        private readonly DbsaleContext _dbContext;

        public SaleRepository(DbsaleContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public new async Task<Sale> Create(Sale sale)
        {
            Sale saleGenerated = new Sale();

            using (var transaccion = _dbContext.Database.BeginTransaction())
            {
                try
                {

                    ///////////remove product from stock/////////
                    foreach (SaleDetails sl in sale.SaleDetails)
                    {
                        Product product_found = _dbContext.Products.Where(p => p.IdProduct == sl.IdProduct).First();
                        product_found.Stock = product_found.Stock - sl.Amount;
                        _dbContext.Update(product_found);
                    }
                    await _dbContext.SaveChangesAsync();
                    /////////////////////////////////////////////

                    ///////////new ID value/////////
                    IdNumber idnumberNext = _dbContext.Idnumbers.First();
                    idnumberNext.LastNumber = idnumberNext.LastNumber + 1;
                    idnumberNext.Timestamp = DateTime.Now;
                    await _dbContext.SaveChangesAsync();
                    /////////////////////////////////////////////


                    int numberOfDigits = 4; //00001
                    string zeros = string.Concat(Enumerable.Repeat("0", numberOfDigits));
                    string iDSaleNumber = zeros + idnumberNext.LastNumber.ToString();

                    iDSaleNumber = iDSaleNumber.Substring(iDSaleNumber.Length - numberOfDigits, numberOfDigits);

                    sale.IdNumber = iDSaleNumber;

                    await _dbContext.AddAsync(sale);
                    await _dbContext.SaveChangesAsync();

                    saleGenerated = sale;

                    transaccion.Commit();

                }
                catch
                {
                    transaccion.Rollback();
                    throw;
                }
            }
            return saleGenerated;
        }
    }
}
