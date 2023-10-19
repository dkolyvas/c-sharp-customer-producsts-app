namespace CustomerProductsApp.Exeptions
{
    public class UserExistsException: Exception
    {
        public UserExistsException(string email)
            :base($"Error: Cannot create user. A user with email {email} already exists.")
        {

        }
        
    }
}
