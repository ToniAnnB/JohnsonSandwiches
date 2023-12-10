using System.Linq.Expressions;

namespace ApplicationAPI.IRespository
{
    public interface IGenericRepository<T> where T : class
    {
        // null is used to make the parameter optional
        Task<IList<T>> GetAllAsync(
            Expression<Func<T, bool>> expression = null, 
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            List<string> includes = null);

        Task<T> Get(
            Expression<Func<T, bool>> expression = null,
            List<string> includes = null);

        void Update(T enity);

        Task<T> Insert(T enity);
        Task<T> InsertRange(IEnumerable<T> enities);

        Task Delete(int id);
        void DeleteRange(IEnumerable<T> enities);
    }
}
