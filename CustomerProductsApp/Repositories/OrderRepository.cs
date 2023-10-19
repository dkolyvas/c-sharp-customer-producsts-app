using CustomerProductsApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerProductsApp.Repositories
{
    public class OrderRepository : BaseRepository<Order>, IOrderRepository
    {
        public OrderRepository(ProductsDbContext db) : base(db)
        {
            
        }

        public async Task<IEnumerable<Order>> GetCustomerOrdersAsync(int id)
        {
            return await _context.Orders.Where(o => o.Customer.Id == id).Include(o =>o.Customer)
                .Include(o=> o.Product).ToListAsync();
        }
    }
}
