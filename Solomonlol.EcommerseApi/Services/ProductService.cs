using Solomonlol.EcommerseApi.Interfaces;
using Solomonlol.EcommerseApi.Models.Base;

namespace Solomonlol.EcommerseApi.Services
{
    public class ProductService : IProductService
    {
        public Task Create(CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task Delete(string name, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task<Product> Get(string name, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }

        public Task Update(Product item, CancellationToken ct = default)
        {
            throw new NotImplementedException();
        }
    }
}
