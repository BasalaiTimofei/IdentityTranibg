using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityTraning.Context;
using IdentityTraning.Interfaces;
using IdentityTraning.Models.DbModel;

namespace IdentityTraning.Repositories
{
    public class PositionRepository : IRepository<Position>
    {
        private readonly ApplicationContext _applicationContext;

        public PositionRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
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

            item.Id = Guid.NewGuid().ToString();

            item.Workers = new List<Worker>();

            _applicationContext.Positions.Add(item);
            _applicationContext.SaveChanges();
        }

        public void Update(Position item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
            _applicationContext.SaveChanges();
        }

        public async Task Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var position = await _applicationContext.Positions.FindAsync(id);
            if (position == null) throw new NullReferenceException();
            _applicationContext.Positions.Remove(position);
            _applicationContext.SaveChanges();
        }
    }
}