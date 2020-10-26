using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Basket.Models;
using Basket.Services;
using CompanyProducts.Models;
using Microsoft.AspNetCore.Mvc;

namespace Basket.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketController : ControllerBase
    {
        private static IBasketRepository _basket;
        private readonly ProductDetailsRepository _productDetailsRepository;
        private readonly CustomerDetailsRepository _customerDetailsRepository;

        public BasketController()
        {
            _basket = new BasketRepository();
            _productDetailsRepository = new ProductDetailsRepository(new ApiHelper(new HttpClient()));
            _customerDetailsRepository = new CustomerDetailsRepository(new ApiHelper(new HttpClient()));
        }

        [HttpPost]
        public void AddToBasket(int customerId, int productId)
        {
            var currentBasket = _basket.GetBasket(customerId);
            if (currentBasket == null)
            {
                currentBasket = new BasketWithGoods
                {
                    CustomerId = customerId
                };
                _basket.AddToBasket(currentBasket);
            }
            currentBasket.ProductIds.Add(productId);

        }

        [HttpGet]
        public async Task<List<Product>> GetBasket(int customerId)
        {
            var currentBasket = _basket.GetBasket(customerId);
            var customerInfo = _customerDetailsRepository.GetCustomer(customerId);
            List<Product> products = new List<Product>();
            foreach (var productId in currentBasket.ProductIds)
            {
                var productDetails = await _productDetailsRepository.GetProduct(productId);
                products.Add(productDetails);
            }

            return products;
        }
    }
}
