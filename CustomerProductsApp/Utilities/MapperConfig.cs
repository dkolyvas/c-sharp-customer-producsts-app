using AutoMapper;
using CustomerProductsApp.Data;
using CustomerProductsApp.DTO;
using CustomerProductsApp.Repositories;

namespace CustomerProductsApp.Utilities
{
    public class MapperConfig: Profile
    {


        public MapperConfig()
        {

            CreateMap<User, UserShowDTO>().ReverseMap();
            CreateMap<Customer, CustomerShowDTO>().ReverseMap();
            CreateMap<Customer, CustomerInsertDTO>().ReverseMap();
            CreateMap<Customer, CustomerUpdateDTO>().ReverseMap();
            CreateMap<Product, ProductShowDTO>().ReverseMap();
            CreateMap<Product, ProductInsertDTO>().ReverseMap();
            CreateMap<Product, ProductUpdateDTO>().ReverseMap();
            CreateMap<Order, OrderInsertDTO>().ReverseMap();
            CreateMap<Order, OrderUpdateDTO>().ReverseMap();
            CreateMap<Order, OrderShowDTO>().ForMember(d => d.CustomerName,
                f => f.MapFrom(s => $"{s.Customer!.Firstname} {s.Customer.Lastname}"))
                .ForMember(d => d.ProductName, f => f.MapFrom(s => s.Product!.Name))
                .ReverseMap();
            //

        }

    }
}
