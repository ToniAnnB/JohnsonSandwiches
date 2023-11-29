using System.Linq.Expressions;

namespace JSandwiches.API.IRespository
{
    public interface IGenericRespository<T> where T : class
    {
        Task<IList<T>> GetAll(
            Expression<Func<T, bool>>? expression, 
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy,
            List<string>? includes);

        //Task<IList<T>> GetAll(
        //    Expression<Func<T, bool>> expression = null, 
        //    Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
        //    List<string> includes = null);

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
