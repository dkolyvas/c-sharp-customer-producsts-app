using CustomerProductsApp.Data;

namespace CustomerProductsApp.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ProductsDbContext _context;

        public UnitOfWork(ProductsDbContext context)
        {
            _context = context;
        }

        public IUserRepository UserRepository => new UserRepository(_context);

        public IBaseRepository<Customer> CustomerRepository => new BaseRepository<Customer>(_context);

        public IBaseRepository<Product> ProductRepository => new BaseRepository<Product>(_context);

        public IOrderRepository OrderRepository => new OrderRepository(_context);

        public async Task<bool> SaveAsync()
        {
            return await _context.SaveChangesAsync() >0;
        }
    }
}
