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
    public class CheckRepository : IRepository<Check>
    {
        private bool _disposed;
        private readonly ApplicationContext _applicationContext;

        public CheckRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
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


        public async Task<IList<Check>> GetAll()
        {
            return await _applicationContext.Checks.ToListAsync();
        }

        public async Task<Check> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var check = await _applicationContext.Checks.FindAsync(id);
            if (check == null) throw new NullReferenceException();
            return check;
        }

        public void Create(Check item)
        {
            if (item?.Customer == null || item.Products == null || item.Shop == null)
                throw new NullReferenceException();

            item.Id = new Guid().ToString();
            item.PurchaseDate = DateTime.Now;

            _applicationContext.Checks.Add(item);
        }

        public void Update(Check item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
        }

        public async Task Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var check = await _applicationContext.Checks.FindAsync(id);
            if (check == null) throw new NullReferenceException();
            _applicationContext.Checks.Remove(check);
        }

        public async Task Save()
        {
            await _applicationContext.SaveChangesAsync();
        }
    }
}