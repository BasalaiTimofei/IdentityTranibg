using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using IdentityTraning.Context;
using IdentityTraning.Interfaces;
using IdentityTraning.Models.DbModel;

namespace IdentityTraning.Repositories
{
    public class UserRepository : IRepository<User>
    {
        private readonly ApplicationContext _applicationContext;

        public UserRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<IList<User>> GetAll()
        {
            return await _applicationContext.Users.ToListAsync();
        }

        public async Task<User> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var user = await _applicationContext.Users.FindAsync(id);
            if (user == null) throw new NullReferenceException();
            return user;
        }

        public async Task<User> GetByName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new NullReferenceException();
            var user = await _applicationContext.Users.Where(w => 
                    string.Equals(w.UserName, name, StringComparison.CurrentCulture)).FirstAsync();
            if (user == null) throw new NullReferenceException();
            return user;
        }

        public void Create(User item)
        {
            if (item == null) throw new NullReferenceException();

            item.Id = Guid.NewGuid().ToString();
            item.JoinDate = DateTime.Now;
            item.Worker = null;
            item.Customer = null;

            _applicationContext.Users.Add(item);
            _applicationContext.SaveChanges();
        }

        public void Update(User item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
            _applicationContext.SaveChanges();
        }

        public async Task Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var user = await _applicationContext.Users.FindAsync(id);
            if (user == null) throw new NullReferenceException();
            _applicationContext.Users.Remove(user);
            _applicationContext.SaveChanges();
        }
    }
}