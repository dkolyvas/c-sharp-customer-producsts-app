using CustomerProductsApp.Data;
using CustomerProductsApp.DTO;

namespace CustomerProductsApp.Services
{
    public interface IUserService
    {

       public Task<UserShowDTO?> RegisterUser(UserRegisterDTO registerDetails);
        public Task<User?> Login(UserLoginDTO credentials);

        public Task<UserShowDTO?> UpdateUser(UserUpdateDTO dto);
        public Task<UserShowDTO?> GetUserByEmail(string email);
    }
}
