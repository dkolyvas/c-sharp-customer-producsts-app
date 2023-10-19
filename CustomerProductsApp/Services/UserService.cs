using AutoMapper;
using CustomerProductsApp.Data;
using CustomerProductsApp.DTO;
using CustomerProductsApp.Exeptions;
using CustomerProductsApp.Repositories;

namespace CustomerProductsApp.Services
{
    public class UserService : IUserService
    {
        
        private IMapper _mapper;
        private IUnitOfWork _repositories;

        public UserService( IMapper mapper, IUnitOfWork repositories)
        {
            
            _mapper = mapper;
            _repositories = repositories;
        }

        public async Task<UserShowDTO?> GetUserByEmail(string email)
        {
            User? user = await _repositories.UserRepository.GetByEmail(email);
            return _mapper.Map<UserShowDTO?>(user);
        }

        public async Task<User?> Login(UserLoginDTO credentials)
        {
            User? user = await _repositories.UserRepository.Login(credentials.Email, credentials.Password);
            return user;
        }

        public async Task<UserShowDTO?> RegisterUser(UserRegisterDTO registerDetails)
        {
            User? user = null;
            if(registerDetails.Password != registerDetails.ConfirmPassword)
            {
                throw new UnableToConfirmPasswordException();
            }
           
            user = await _repositories.UserRepository.RegisterUser(registerDetails);
            await _repositories.SaveAsync();
            return _mapper.Map<UserShowDTO?>(user);
        }

        public async Task<UserShowDTO?> UpdateUser(UserUpdateDTO dto)
        {
            User? user = null;
            
            if(dto.NewPassword != dto.ConfirmPassword)
            {
                throw new UnableToConfirmPasswordException();
            }
            
            user = await _repositories.UserRepository.UpdateUser(dto.Email, dto.OldPassword, dto.NewPassword);
            await _repositories.SaveAsync();
            return _mapper.Map<UserShowDTO?>(user);
        }
    }
}
