using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityTraning.Context;
using IdentityTraning.Interfaces;
using IdentityTraning.Models;
using IdentityTraning.Models.DbModel;

namespace IdentityTraning.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        private readonly ApplicationContext _applicationContext;
        private bool _disposed;

        public CustomerRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<IList<Customer>> GetAll()
        {
            return await _applicationContext.Customers.ToListAsync();
        }

        public async Task<Customer> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var customer = await _applicationContext.Customers.FindAsync(id);
            if (customer == null) throw new NullReferenceException();
            return customer;
        }

        public void Create(Customer customer)
        {
            customer.Checks = new List<Check>();
            customer.Id = new Guid().ToString();

            _applicationContext.Customers.Add(customer);
        }

        public void Update(Customer customer)
        {
            _applicationContext.Entry(customer).State = EntityState.Modified;
        }

        public async Task Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var customer = await _applicationContext.Customers.FindAsync(id);
            if (customer == null) throw new NullReferenceException();
            _applicationContext.Customers.Remove(customer);
        }

        public async Task Save()
        {
            await _applicationContext.SaveChangesAsync();
        }


        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _applicationContext.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}