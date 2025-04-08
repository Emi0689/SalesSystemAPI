using SalesSystem.DAL.DBContext;
using SalesSystem.Model.Entities;
using SalesSystem.DAL.Repositories.Interfaces;

namespace SalesSystem.DAL.Repositories
{
    public class SaleRepository: GenericRepository<Sale>, ISaleRepository
    {
        private readonly DbsaleContext _dbsaleContext;

        public SaleRepository(DbsaleContext dbContext) : base(dbContext)
        {
            _dbsaleContext = dbContext;
        }

        public new async Task<Sale> Create(Sale sale)
        {
            Sale saleGenerated = new Sale();

            using(var transaccion = _dbsaleContext.Database.BeginTransaction())
            {
                try
                {
  
                    ///////////remove product from stock/////////
                    foreach (SaleDetails sl in sale.SaleDetails)
                    {
                        Product product_found = _dbsaleContext.Products.Where(p => p.IdProduct == sl.IdProduct).First();
                        product_found.Stock = product_found.Stock - sl.Amount;
                        _dbsaleContext.Update(product_found);
                    }
                    await _dbsaleContext.SaveChangesAsync();
                    /////////////////////////////////////////////

                    ///////////new ID value/////////
                    Idnumber idnumberNext = _dbsaleContext.Idnumbers.First();
                    idnumberNext.LastNumber = idnumberNext.LastNumber + 1;
                    idnumberNext.Timestamp = DateTime.Now;
                    await _dbsaleContext.SaveChangesAsync();
                    /////////////////////////////////////////////


                    int numberOfDigits = 4; //00001
                    string zeros = string.Concat(Enumerable.Repeat("0", numberOfDigits));
                    string iDSaleNumber = zeros + idnumberNext.LastNumber.ToString();

                    iDSaleNumber = iDSaleNumber.Substring(iDSaleNumber.Length - numberOfDigits, numberOfDigits);

                    sale.Idnumber = iDSaleNumber;

                    await _dbsaleContext.AddAsync(sale);
                    await _dbsaleContext.SaveChangesAsync();

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
