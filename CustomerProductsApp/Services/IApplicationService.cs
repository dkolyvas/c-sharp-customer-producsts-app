namespace CustomerProductsApp.Services
{
    public interface IApplicationService
    {
        public IUserService UserService { get; }    
        public IProductService ProductService { get; }
        public ICustomerService CustomerService { get; }
        public IOrderService OrderService { get; }
    }
}
