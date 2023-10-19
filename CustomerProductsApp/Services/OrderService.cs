using AutoMapper;
using CustomerProductsApp.Data;
using CustomerProductsApp.DTO;
using CustomerProductsApp.Exeptions;
using CustomerProductsApp.Repositories;

namespace CustomerProductsApp.Services
{
    public class OrderService: IOrderService
    {
        private IMapper _mapper;
        private IUnitOfWork _repositories;

        public OrderService(IMapper mapper, IUnitOfWork repositories)
        {
            _mapper = mapper;
            _repositories = repositories;
        }

        public async Task<OrderShowDTO> AddNew(OrderInsertDTO dto)
        {
            Order order = _mapper.Map<Order>(dto);  
            Customer customer = await _repositories.CustomerRepository.GetAsync(dto.CustomerId!);
            Product product = await _repositories.ProductRepository.GetAsync(dto.ProductId);
            order.Customer = customer;
            order.Product = product;
            Order insertedOrder = await _repositories.OrderRepository.AddAsync(order);
            await _repositories.SaveAsync();
            return _mapper.Map<OrderShowDTO>(order);
        }

        public async Task<bool> Delete(int id)
        {
            bool success = await _repositories.OrderRepository.DeleteAsync(id);
            if (!success) throw new EntityNotFoundException("order");
            await _repositories.SaveAsync();
            return true;
        }

        public async Task<List<OrderShowDTO>> GetByCustomerId(int c_id)
        {
            List<OrderShowDTO> results = new();
            var data = await _repositories.OrderRepository.GetCustomerOrdersAsync(c_id);
            foreach(var order in data)
            {
                OrderShowDTO dto = _mapper.Map<OrderShowDTO>(order);
               /* dto.CustomerName = order.Customer.Firstname + " " + order.Customer.Lastname;
                dto.ProductName = order.Product.Name;*/
                results.Add(dto);
            }
            return results;
        }

        public async Task<OrderUpdateDTO> GetById(int id)
        {
            Order? order = await _repositories.OrderRepository.GetAsync(id);
            if (order is null) throw new EntityNotFoundException("order");
            return _mapper.Map<OrderUpdateDTO>(order);
        }

        public async Task<OrderShowDTO> Update(OrderUpdateDTO dto)
        {
            Product? product = null;
            Customer? customer = null;
            Order updatedOrder = _mapper.Map<Order>(dto);
            Order? order = await _repositories.OrderRepository.GetAsync(dto.Id);
            if (order is null) throw new EntityNotFoundException("order");
            product = await _repositories.ProductRepository.GetAsync(dto.ProductId);
            customer = await _repositories.CustomerRepository.GetAsync(dto.CustomerId);
            updatedOrder.Customer = customer;
            updatedOrder.Product = product;
            await _repositories.OrderRepository.UpdateAsync(updatedOrder, dto.Id);
            await _repositories.SaveAsync();
            return _mapper.Map<OrderShowDTO>(updatedOrder);

        }
    }
}
