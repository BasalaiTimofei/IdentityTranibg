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
    public class PositionRepository : IRepository<Position>
    {
        private bool _disposed;
        private readonly ApplicationContext _applicationContext;

        public PositionRepository(ApplicationContext applicationContext)
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



        public async Task<IList<Position>> GetAll()
        {
            return await _applicationContext.Positions.ToListAsync();
        }

        public async Task<Position> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var position = await _applicationContext.Positions.FindAsync(id);
            if (position == null) throw new NullReferenceException();
            return position;
        }

        public void Create(Position item)
        {
            if (item == null) throw new NullReferenceException();
            item.Id = new Guid().ToString();
            item.Shops = new List<Shop>();
            item.Workers = new List<Worker>();

            _applicationContext.Positions.Add(item);
        }

        public void Update(Position item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
        }

        public async Task Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var position = await _applicationContext.Positions.FindAsync(id);
            if (position == null) throw new NullReferenceException();
            _applicationContext.Positions.Remove(position);
        }

        public async Task Save()
        {
            await _applicationContext.SaveChangesAsync();
        }
    }
}