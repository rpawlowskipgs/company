using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CompanyProducts.Models;

namespace CompanyProducts.Services
{
    public class ProductRepository : IProductRepository
    {
        private List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>
            {
                new Product() { Id = 1, Name = "Super", Price = 100 },
                new Product() { Id = 2, Name = "Fancy", Price = 90 },
                new Product() { Id = 3, Name = "Clancy", Price = 50 }
            };
        }

        public List<Product> GetProducts()
        {
            return _products;
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.Id == id);
        }

        public void AddProduct(Product product)
        {
            product.Id = _products.Select(p => p.Id).Max() + 1;
            _products.Add(product);
        }

        public void UpdateProduct(int id, Product product)
        {
            var productToUpdate = _products.FirstOrDefault(p => p.Id == id);

            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
            }
        }

        public void DeleteProduct(int id)
        {
            var productToDelete = _products.FirstOrDefault(p => p.Id == id);

            if (productToDelete != null)
                _products.Remove(productToDelete);
        }
    }
}
