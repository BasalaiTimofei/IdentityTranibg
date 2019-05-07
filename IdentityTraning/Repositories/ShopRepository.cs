﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using IdentityTraning.Context;
using IdentityTraning.Interfaces;
using IdentityTraning.Models;
using IdentityTraning.Models.DbModel;

namespace IdentityTraning.Repositories
{
    public class ShopRepository : IRepository<Shop>
    {
        private readonly ApplicationContext _applicationContext;
        public ShopRepository(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public async Task<IList<Shop>> GetAll()
        {
            return await _applicationContext.Shops.ToListAsync();
        }

        public async Task<Shop> GetById(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();

            var shop = await _applicationContext.Shops.FindAsync(id);

            if (shop == null) throw new NullReferenceException();

            return shop;
        }

        public void Create(Shop shop)
        {
            shop.Workers = new List<Worker>();
            shop.Checks = new List<Check>();
            shop.Products = new List<Product>();
            shop.Id = new Guid().ToString();

            _applicationContext.Shops.Add(shop);
        }

        public void Update(Shop shop)
        {
            _applicationContext.Entry(shop).State = EntityState.Modified;
        }

        public async Task Delete(string id)
        {
            if (string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();
            var shop = await _applicationContext.Shops.FindAsync(id);
            if (shop == null) throw new NullReferenceException();
            _applicationContext.Shops.Remove(shop);
        }

        public async Task Save()
        {
            await _applicationContext.SaveChangesAsync();
        }

        private bool _disposed;

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