namespace CustomerProductsApp.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetAsync(int id);
        Task<T?> UpdateAsync(T entity, int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<T?> AddAsync(T entity);
        Task<bool> DeleteAsync(int id);
    }
}
