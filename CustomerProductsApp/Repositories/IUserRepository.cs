using CustomerProductsApp.Data;
using CustomerProductsApp.DTO;

namespace CustomerProductsApp.Repositories
{
    public interface IUserRepository
    {
        public Task<User> RegisterUser(UserRegisterDTO dto);
        public  Task<User?> Login(string email, string password);
        public Task<User?> GetByEmail(string email);
        public Task<User?> UpdateUser(string email, string oldPassword, string newPassword);
    }
}
