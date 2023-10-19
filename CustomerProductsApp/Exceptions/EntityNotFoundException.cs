namespace CustomerProductsApp.Exeptions
{
    public class EntityNotFoundException : Exception
    {
        public EntityNotFoundException(string entityName): base($"The requested {entityName} wasn't found")
        {
        }
    }
}
