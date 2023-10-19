using CustomerProductsApp.Data;
using Microsoft.EntityFrameworkCore;

namespace CustomerProductsApp.Repositories
{
    public  class BaseRepository<T>:IBaseRepository<T> where T : class
    {
        protected ProductsDbContext _context;
        protected  DbSet<T> _dataSet;

        public BaseRepository(ProductsDbContext db)
        {
            _context = db;
            _dataSet = _context.Set<T>();
        }

        public virtual async Task<T?> AddAsync(T entity)
        {
           await _dataSet.AddAsync(entity);
            return entity;

        }

        public virtual async Task<bool> DeleteAsync(int id)
        {
            T? entity = await _dataSet.FindAsync(id);
            if (entity is null) return false;
            _dataSet.Remove(entity);
            return true;
        }

        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await  _dataSet.ToListAsync();
            
        }

        public virtual async Task<T?> GetAsync(int id)
        {
            T? result = await _dataSet.FindAsync(id);
            return result;
        }

        public virtual async Task<T?> UpdateAsync(T entity, int id)
        {
            
            /*_context.Entry(entity).State = EntityState.Modified;
            _dataSet.Update(entity);*/
          var val = await _dataSet.FindAsync(id);
            if (val is not null)
            {
                _context.Entry(val).CurrentValues.SetValues(entity);
                _context.Entry(val).State = EntityState.Modified;
            }
            return val;
         
            
            
            
        }
    }
}
