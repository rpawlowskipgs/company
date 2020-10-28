﻿using System;
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
                new Product { ProductId = 1, Name = "Super", Price = 100 },
                new Product { ProductId = 2, Name = "Fancy", Price = 90 },
                new Product { ProductId = 3, Name = "Clancy", Price = 50 }
            };
        }

        public List<Product> GetProducts()
        {
            return _products;
        }

        public Product GetProductById(int id)
        {
            return _products.FirstOrDefault(p => p.ProductId == id);
        }

        public void AddProduct(Product product)
        {
            // add lock here and in customers

            product.ProductId = _products.Select(p => p.ProductId).Max() + 1;
            _products.Add(product);
        }

        public void UpdateProduct(int id, Product product)
        {
            var productToUpdate = _products.FirstOrDefault(p => p.ProductId == id);

            if (productToUpdate != null)
            {
                productToUpdate.Name = product.Name;
                productToUpdate.Price = product.Price;
            }
        }

        public void DeleteProduct(int id)
        {
            var productToDelete = _products.FirstOrDefault(p => p.ProductId == id);

            if (productToDelete != null)
                _products.Remove(productToDelete);
        }
    }
}
