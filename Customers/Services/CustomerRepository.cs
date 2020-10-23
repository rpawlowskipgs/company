using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Customers.Models;

namespace Customers.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private List<Customer> _customers;

        public CustomerRepository()
        {
            _customers = new List<Customer>
            {
                new Customer { CustomerId = 1, FirstName = "John", LastName = "Kowalyskey", Age = 59 },
                new Customer { CustomerId = 2, FirstName = "Grażyna", LastName = "Nowak", Age = 21 },
                new Customer { CustomerId = 1, FirstName = "Janusz", LastName = "Andruszkiewicz", Age = 32 }
            };
        }

        public List<Customer> GetCustomers()
        {
            return _customers;
        }

        public Customer GetCustomerById(int id)
        {
            return _customers.FirstOrDefault(c => c.CustomerId == id);
        }

        public void AddCustomer(Customer customer)
        {
            customer.CustomerId = _customers.Select(c => c.CustomerId).Max() + 1;
            _customers.Add(customer);
        }

        public void UpdateCustomer(int id, Customer customer)
        {
            var customerToUpdate = _customers.FirstOrDefault(c => c.CustomerId == id);

            if (customerToUpdate != null)
            {
                customerToUpdate.FirstName = customer.FirstName;
                customerToUpdate.LastName = customer.LastName;
                customerToUpdate.Age = customer.Age;
            }
        }

        public void DeleteCustomer(int id)
        {
            var customerToDelete = _customers.FirstOrDefault(c => c.CustomerId == id);

            if (customerToDelete != null)
                _customers.Remove(customerToDelete);
        }
    }
}
