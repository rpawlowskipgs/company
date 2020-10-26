using System;
using System.Threading.Tasks;
using CompanyProducts.Models;

namespace Basket.Services
{
    public class ProductDetailsRepository
    {
        private readonly ApiHelper _apiHelper;

        public ProductDetailsRepository(ApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }
        public async Task<Product> GetProduct(int productId)
        {
            return await _apiHelper.Get<Product>(new Uri($"http://localhost:57856/products/{productId}"));
        }
    }
}
