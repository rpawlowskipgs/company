using System.Collections.Generic;
using System.Linq;
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
        private readonly IBasketRepository _basket;
        private readonly IProductDetailsRepository _productDetailsRepository;
        private readonly ICustomerDetailsRepository _customerDetailsRepository;

        public BasketController(IProductDetailsRepository productDetailsRepository,
                                ICustomerDetailsRepository customerDetailsRepository,
                                IBasketRepository basket)
        {
            _basket = basket;
            _productDetailsRepository = productDetailsRepository;
            _customerDetailsRepository = customerDetailsRepository;
        }

        [HttpPost]
        public void AddToBasket(int customerId, int productId)
        {
            var currentBasket = _basket.GetBasket(customerId);
            if (currentBasket == null)
            {
                currentBasket = new BasketWithGoods
                {
                    CustomerId = customerId,
                    ProductIds = new List<int>(productId)
                };
                _basket.AddToBasket(currentBasket);
            }
            else
            {
                currentBasket.Quantity = productId;
                //add number of elements to the list
            }
            //productId is not added to the list of all prods!
            currentBasket.ProductIds.Add(productId);

        }

        [HttpGet]
        public async Task<BasketResponse> GetBasket(int customerId)
        {
            var currentBasket = _basket.GetBasket(customerId);

            BasketResponse basketResponse = new BasketResponse();

            var customerInfo = await _customerDetailsRepository.GetCustomer(customerId);

            List<Task<Product>> products = new List<Task<Product>>();
            foreach (var productId in currentBasket.ProductIds)
            {
                var productDetails = _productDetailsRepository.GetProduct(productId);
                products.Add(productDetails);
            }
            await Task.WhenAll(products);

            basketResponse.Product = products.Select(p => p.Result).ToList();
            basketResponse.Customer = customerInfo;
            return basketResponse;
        }

        [HttpDelete]
        public StatusCodeResult RemoveFromBasket(int customerId, int productId)
        {
            var currentBasket = _basket.GetBasket(customerId);
            if (currentBasket != null)
            {
                var productToDelete = currentBasket.ProductIds.FirstOrDefault(p => p == productId);
                var isDeleted = currentBasket.ProductIds.Remove(productToDelete);

                if (isDeleted)
                {
                    return Ok();
                }
            }
            return NotFound();
        }
    }
}
