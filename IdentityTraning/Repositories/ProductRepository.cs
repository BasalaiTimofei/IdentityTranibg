using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityTraning.Context;
using IdentityTraning.Interfaces;
using IdentityTraning.Models.DbModel;

namespace IdentityTraning.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly ApplicationContext _applicationContext;

        public ProductRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<IList<Product>> GetAll()
        {
            return await _applicationContext.Products.ToListAsync();
        }

        public async Task<Product> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var product = await _applicationContext.Products.FindAsync(id);
            if(product == null) throw new NullReferenceException();
            return product;
        }

        public void Create(Product item)
        {
            if (item == null) throw new NullReferenceException();

            item.Id = Guid.NewGuid().ToString();

            item.Checks = new List<Check>();

            _applicationContext.Products.Add(item);
            _applicationContext.SaveChanges();
        }

        public void Update(Product item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
            _applicationContext.SaveChanges();
        }

        public async Task Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var product = await _applicationContext.Products.FindAsync(id);
            if (product == null) throw new NullReferenceException();
            _applicationContext.Products.Remove(product);
            _applicationContext.SaveChanges();
        }
    }
}