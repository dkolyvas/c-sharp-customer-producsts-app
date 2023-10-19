using CustomerProductsApp.DTO;

namespace CustomerProductsApp.Services
{
    public interface ICustomerService
    {
        public Task<List<CustomerShowDTO>> GetAll();
        public Task<CustomerUpdateDTO> GetById(int id);
        public Task<CustomerShowDTO> AddNew(CustomerInsertDTO dto);
        public Task<CustomerShowDTO> Update(CustomerUpdateDTO dto);
        public Task<bool> Delete(int id);
    }
}
