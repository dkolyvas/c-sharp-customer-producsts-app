using AutoMapper;
using CustomerProductsApp.Data;
using CustomerProductsApp.DTO;
using CustomerProductsApp.Exeptions;
using CustomerProductsApp.Repositories;

namespace CustomerProductsApp.Services
{
    public class CustomerService :ICustomerService
    {
        private IUnitOfWork _repositories;
        private IMapper _mapper;

        public CustomerService(IUnitOfWork repositories, IMapper mapper)
        {
            _repositories = repositories;
            _mapper = mapper;
        }

        public async Task<CustomerShowDTO> AddNew(CustomerInsertDTO dto)
        {
            Customer customer = _mapper.Map<Customer>(dto);
            Customer insertedCustomer = await _repositories.CustomerRepository.AddAsync(customer);
            await _repositories.SaveAsync();
            return _mapper.Map<CustomerShowDTO>(insertedCustomer);

        }

        public async Task<bool> Delete(int id)
        {
            bool success = await _repositories.CustomerRepository.DeleteAsync(id);
            await _repositories.SaveAsync();
            if(!success) throw new EntityNotFoundException("customer");
            return success;
        }

        public async Task<List<CustomerShowDTO>> GetAll()
        {
            List<CustomerShowDTO> results = new();
            var data = await _repositories.CustomerRepository.GetAllAsync();
            foreach ( var item in data)
            {
                CustomerShowDTO currCustomer = _mapper.Map<CustomerShowDTO>(item);
                results.Add(currCustomer);
            }
            return results;
        }

        public async Task<CustomerUpdateDTO> GetById(int id)
        {
            Customer? customer = await _repositories.CustomerRepository.GetAsync(id);
            if (customer is null) throw new EntityNotFoundException("customer");
            return _mapper.Map<CustomerUpdateDTO>(customer);
        }

        public async Task<CustomerShowDTO> Update(CustomerUpdateDTO dto)
        {
            Customer? oldcustomer = await _repositories.CustomerRepository.GetAsync(dto.Id);
            if (oldcustomer is null) throw new EntityNotFoundException("customer" + dto.Id);
            Customer? customer = _mapper.Map<Customer>(dto);
           await  _repositories.CustomerRepository.UpdateAsync(customer, dto.Id);
            await _repositories.SaveAsync();
            return _mapper.Map<CustomerShowDTO>(customer);
            

        }
    }
}
