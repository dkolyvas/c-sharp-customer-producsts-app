using CustomerProductsApp.Data;
using CustomerProductsApp.DTO;
using CustomerProductsApp.Exeptions;
using CustomerProductsApp.Security;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace CustomerProductsApp.Repositories
{
    public class UserRepository :BaseRepository<User>, IUserRepository
    {
        public UserRepository(ProductsDbContext db) : base(db)
        {
        }

        public async Task<User> RegisterUser(UserRegisterDTO dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(us => us.Email == dto.Email);
            string password = Encryption.Encrypt(dto.Password!);
            if (user is not null) throw new UserExistsException(dto.Email!);
            user = new User { Email = dto.Email!, Password = password };
            await _dataSet.AddAsync(user);
            return user;
        }

        public async Task<User?> Login(string email, string password)
        {
            User? user = await _context.Users.FirstOrDefaultAsync(us => us.Email == email);
            if (user is null) return null;
            bool passwordOK = Encryption.confirmPassword(password, user.Password);
            if (!passwordOK) return null;
            return user;
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>u.Email == email);  
        }

        public async Task<User?> UpdateUser(string email, string oldPassword, string newPassword)
        {
            User? user =await  _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user is null) throw new EntityNotFoundException("user");
            if (!Encryption.confirmPassword(oldPassword, user.Password))
            {
                throw new IncorrectPasswordException();
            }
            user.Password = Encryption.Encrypt(newPassword);
            UpdateAsync(user, user.Id);
            return user; 
        }
    }
}
