using System.Collections.Generic;
using System.Linq;
using CompanyProducts.Models;
using Microsoft.AspNetCore.Mvc;

namespace CompanyProducts.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductsController : ControllerBase
    {
        private static List<Product> _products = new List<Product>
        {
            new Product() { Id = 1, Name = "Super", Price = 100 },
            new Product() { Id = 2, Name = "Fancy", Price = 90 },
            new Product() { Id = 3, Name = "Clancy", Price = 50 },
        };

        [HttpGet]
        public IEnumerable<Product> Get()
        {
            return _products;
        }

        [HttpGet("{id}")]
        public Product Get(int id)
        {
            return _products.FirstOrDefault(x => x.Id == id);
        }

        [HttpPost]
        public Product Post([FromBody] Product product)
        {
            product.Id = _products.Select(i => i.Id).Max() + 1;
            _products.Add(product);

            return product;
        }

        [HttpPut("{id}")]
        public Product Put(int id, [FromBody] Product product)
        {
            var productToUpdate = _products.FirstOrDefault(p => p.Id == id);

            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
            }

            return productToUpdate;
        }

        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            var productToDelete = _products.FirstOrDefault(p => p.Id == id);

            if (productToDelete != null)
                _products.Remove(productToDelete);
        }
    }
}
