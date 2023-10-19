using CustomerProductsApp.Data;

namespace CustomerProductsApp.Repositories
{
    public interface IUnitOfWork
    {
        public IUserRepository UserRepository { get; }
        public IBaseRepository<Customer> CustomerRepository { get; }
        public IBaseRepository<Product> ProductRepository { get; }
        public IOrderRepository OrderRepository { get; }

        public Task<bool> SaveAsync();
    }
}
