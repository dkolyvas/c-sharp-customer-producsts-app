using AutoMapper;
using CustomerProductsApp.Repositories;

namespace CustomerProductsApp.Services
{
    public class ApplicationService : IApplicationService
    {
        private IMapper _mapper;
        private IUnitOfWork _repositories;

        public ApplicationService(IMapper mapper, IUnitOfWork repositories)
        {
            _mapper = mapper;
            _repositories = repositories;
        }

        public IUserService UserService => new UserService(_mapper, _repositories);

        public IProductService ProductService => new ProductService(_mapper, _repositories);

        public ICustomerService CustomerService => new CustomerService(_repositories, _mapper);

        public IOrderService OrderService => new OrderService(_mapper, _repositories);
    }
}
