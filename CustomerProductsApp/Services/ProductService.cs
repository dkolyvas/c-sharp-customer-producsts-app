using AutoMapper;
using CustomerProductsApp.Data;
using CustomerProductsApp.DTO;
using CustomerProductsApp.Exeptions;
using CustomerProductsApp.Repositories;

namespace CustomerProductsApp.Services
{
    public class ProductService: IProductService
    {
        private IMapper _mapper;
        private IUnitOfWork _repositories;

        public ProductService(IMapper mapper, IUnitOfWork repositories)
        {
            _mapper = mapper;
            _repositories = repositories;
        }

        public async Task<ProductShowDTO?> AddNew(ProductInsertDTO dto)
        {
            Product product = _mapper.Map<Product>(dto);
            Product? insertedProduct = await _repositories.ProductRepository.AddAsync(product);
            await _repositories.SaveAsync();
            return _mapper.Map<ProductShowDTO?>(insertedProduct);
        }

        public async Task<bool> Delete(int id)
        {
            bool success = await _repositories.ProductRepository.DeleteAsync(id);
            if (!success) throw new EntityNotFoundException("product");
            await _repositories.SaveAsync();
            return success;
        }

        public async Task<List<ProductShowDTO>> GetAll()
        {
            List<ProductShowDTO> results = new();
            var data = await _repositories.ProductRepository.GetAllAsync();
            foreach( var item in data)
            {
                ProductShowDTO currentProduct = _mapper.Map<ProductShowDTO>(item);
                results.Add(currentProduct);
            }
            return results;
        }

        public async Task<ProductUpdateDTO> GetById(int id)
        {
            Product? product = await _repositories.ProductRepository.GetAsync(id);
            if (product is null) throw new EntityNotFoundException("product");
            return _mapper.Map<ProductUpdateDTO>(product);
        }

        public async Task<ProductShowDTO> Update(ProductUpdateDTO dto)
        {
            Product? oldProduct = await _repositories.ProductRepository.GetAsync(dto.Id);
            if (oldProduct is null) throw new EntityNotFoundException("product");
            Product updatedProduct = _mapper.Map<Product>(dto);
            await _repositories.ProductRepository.UpdateAsync(updatedProduct, dto.Id);
            await _repositories.SaveAsync();
            return _mapper.Map<ProductShowDTO>(updatedProduct);
        }
    }
}
