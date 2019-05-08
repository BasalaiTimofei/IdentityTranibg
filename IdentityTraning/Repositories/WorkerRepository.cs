using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityTraning.Context;
using IdentityTraning.Interfaces;
using IdentityTraning.Models.DbModel;

namespace IdentityTraning.Repositories
{
    public class WorkerRepository : IRepository<Worker>
    {
        private readonly ApplicationContext _applicationContext;

        public WorkerRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<IList<Worker>> GetAll()
        {
            return await _applicationContext.Workers.ToListAsync();
        }

        public async Task<Worker> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var worker = await _applicationContext.Workers.FindAsync(id);
            if (worker == null) throw new NullReferenceException();
            return worker;
        }

        public void Create(Worker item)
        {
            if (item?.Position == null || item.User == null) throw new NullReferenceException();
            item.Id = new Guid().ToString();
            _applicationContext.Workers.Add(item);
        }

        public void Update(Worker item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
        }

        public async Task Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var worker = await _applicationContext.Workers.FindAsync(id);
            if (worker == null) throw new NullReferenceException();
            _applicationContext.Workers.Remove(worker);
        }
    }
}