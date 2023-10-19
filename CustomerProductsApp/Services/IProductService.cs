using CustomerProductsApp.DTO;

namespace CustomerProductsApp.Services
{
    public interface IProductService
    {
        public Task<List<ProductShowDTO>> GetAll();
        public Task<ProductUpdateDTO> GetById(int id);
        public Task<ProductShowDTO?> AddNew(ProductInsertDTO dto);
        public Task<ProductShowDTO> Update(ProductUpdateDTO dto);
        public Task<bool> Delete(int id);
    }
}
