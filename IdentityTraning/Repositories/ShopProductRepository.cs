using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityTraning.Context;
using IdentityTraning.Interfaces;
using IdentityTraning.Models.DbModel;

namespace IdentityTraning.Repositories
{
    public class ShopProductRepository : IRepository<ShopProduct>
    {
        private readonly ApplicationContext _applicationContext;

        public ShopProductRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<IList<ShopProduct>> GetAll()
        {
            return await _applicationContext.ShopProducts.ToListAsync();
        }

        public async Task<ShopProduct> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var shopProduct = await _applicationContext.ShopProducts.FindAsync(id);
            if (shopProduct == null) throw new NullReferenceException();
            return shopProduct;
        }

        public void Create(ShopProduct item)
        {
            if (item?.Price == null || item.Product == null || item.Shop == null) throw new NullReferenceException();
            if (item.Count == 0) item.Count = 1;
            item.Id = new Guid().ToString();

            _applicationContext.ShopProducts.Add(item);
        }

        public void Update(ShopProduct item)
        {
            _applicationContext.Entry(item).State = EntityState.Modified;
        }

        public async Task Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var shopProduct = await _applicationContext.ShopProducts.FindAsync(id);
            if (shopProduct == null) throw new NullReferenceException();
            _applicationContext.ShopProducts.Remove(shopProduct);
        }
    }
}