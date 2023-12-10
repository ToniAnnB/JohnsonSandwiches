using System.Linq.Expressions;

namespace JSandwiches.API.IRespository
{
    public interface IGenericRepository<T> where T : class
    {
        Task<IList<T>> GetAll(
            Expression<Func<T, bool>>? expression, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            List<string>? includes);

        Task<T> Get(
            Expression<Func<T, bool>>? expression,
            List<string>? includes);

        void Update(T enity);

        Task Insert(T enity);
        Task InsertRange(IEnumerable<T> enities);

        Task Delete(int id);
        void DeleteRange(IEnumerable<T> enities);
    }
}
