using CustomerProductsApp.DTO;

namespace CustomerProductsApp.Services
{
    public interface IOrderService
    {
        public Task<List<OrderShowDTO>> GetByCustomerId(int c_id);
        public Task<OrderUpdateDTO> GetById(int id);
        public Task<OrderShowDTO> AddNew(OrderInsertDTO dto);
        public Task<OrderShowDTO> Update(OrderUpdateDTO dto);
        public Task<bool> Delete(int id);
    }
}
