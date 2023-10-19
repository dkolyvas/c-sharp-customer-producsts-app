namespace CustomerProductsApp.Exeptions
{
    public class UnableToConfirmPasswordException : Exception
    {
        public UnableToConfirmPasswordException():base("You must write twice the correct password")
        {
        }
    }
}
