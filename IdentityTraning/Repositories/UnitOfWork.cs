using System;
using System.Threading.Tasks;
using IdentityTraning.Context;

namespace IdentityTraning.Repositories
{
    public class UnitOfWork : IDisposable
    {
        private bool _disposed;
        private readonly ApplicationContext _applicationContext;

        private CheckRepository _checkRepository;
        private CustomerRepository _customerRepository;
        private PositionRepository _positionRepository;
        private ProductRepository _productRepository;
        private ShopRepository _shopRepository;
        private WorkerRepository _workerRepository;
        private ShopProductRepository _shopProductRepository;
        private UserRepository _userRepository;

        public UnitOfWork(ApplicationContext applicationContext)
        {
            _applicationContext = applicationContext;
        }

        public UserRepository UserRepository =>
            _userRepository ?? (_userRepository = new UserRepository(_applicationContext));

        public CheckRepository CheckRepository =>
            _checkRepository ?? (_checkRepository = new CheckRepository(_applicationContext));

        public CustomerRepository CustomerRepository =>
            _customerRepository ?? (_customerRepository = new CustomerRepository(_applicationContext));

        public PositionRepository PositionRepository =>
            _positionRepository ?? (_positionRepository = new PositionRepository(_applicationContext));

        public ProductRepository ProductRepository =>
            _productRepository ?? (_productRepository = new ProductRepository(_applicationContext));

        public ShopRepository ShopRepository =>
            _shopRepository ?? (_shopRepository = new ShopRepository(_applicationContext));

        public WorkerRepository WorkerRepository =>
            _workerRepository ?? (_workerRepository = new WorkerRepository(_applicationContext));

        public ShopProductRepository ShopProductRepository =>
            _shopProductRepository ?? (_shopProductRepository = new ShopProductRepository(_applicationContext));

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