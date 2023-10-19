namespace CustomerProductsApp.Exeptions
{
    public class IncorrectPasswordException : Exception
    {
        public IncorrectPasswordException():base("You have provided an incorrect password")
        {
        }
    }
}
