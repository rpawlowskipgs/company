using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customers.Models;

namespace Basket.Services
{
    public class CustomerDetailsRepository
    {
        private readonly ApiHelper _apiHelper;

        public CustomerDetailsRepository(ApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task<Customer> GetCustomer(int customerId)
        {
            return await _apiHelper.Get<Customer>(new Uri($"http://localhost:51093/customers/{customerId}"));
        }
    }
}
