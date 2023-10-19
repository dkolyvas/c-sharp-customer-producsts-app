using CustomerProductsApp.Data;

namespace CustomerProductsApp.Repositories
{
    public interface IOrderRepository
    {
        Task<Order?> GetAsync(int id);
        Task<Order?> UpdateAsync(Order order, int id);
        Task<IEnumerable<Order>> GetCustomerOrdersAsync(int id);
        Task<Order?> AddAsync(Order order);
        Task<bool> DeleteAsync(int id);
    }
}
