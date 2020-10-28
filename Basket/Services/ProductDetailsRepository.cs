﻿using System;
using System.Threading.Tasks;
using Basket.Configuration;
using CompanyProducts.Models;
using Microsoft.Extensions.Options;

namespace Basket.Services
{
    public class ProductDetailsRepository : IProductDetailsRepository
    {
        private readonly ApiHelper _apiHelper;
        private readonly IOptions<ProductConfiguration> _settingsUrl;


        public ProductDetailsRepository(ApiHelper apiHelper, IOptions<ProductConfiguration> settingsUrl)
        {
            _apiHelper = apiHelper;
            _settingsUrl = settingsUrl;
        }
        public async Task<Product> GetProduct(int productId)
        {
            await Task.Delay(1000);
            return await _apiHelper.Get<Product>(new Uri(string.Format(_settingsUrl.Value.ServiceUrlConfiguration, productId)));
        }
    }
}
