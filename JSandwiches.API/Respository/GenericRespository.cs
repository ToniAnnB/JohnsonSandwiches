using JSandwiches.API.IRespository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using JSandwiches.Models.Data;

namespace JSandwiches.API.Respository
{
    public class GenericRespository<T> : IGenericRespository<T> where T : class
    {
        private readonly ApplicationDbContext _context;
        private readonly DbSet<T> _db;

        public GenericRespository(ApplicationDbContext context)
        {
            _context = context;
            _db = _context.Set<T>();
        }


        public async Task Delete(int id)
        {
            var entity = await _db.FindAsync(id);
            if (entity == null)
            {
                return;
            }
            _db.Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> enities)
        {
            _db.RemoveRange(enities);
        }

        public async Task<T> Get(Expression<Func<T, bool>>? expression, List<string>? includes)
        {
            IQueryable<T> query = _db;
            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }
            return await query.FirstOrDefaultAsync(expression);
        }


        public async Task<IList<T>> GetAll(Expression<Func<T, bool>>? expression, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy, List<string>? includes)
        {
            IQueryable<T> query = _db;

            if (expression != null)
            {
                query = query.Where(expression);
            }

            if (includes != null)
            {
                foreach (var includeProperty in includes)
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return await query.ToListAsync();
        }

        public async Task Insert(T entity)
        {
           
            await _db.AddAsync(entity);

        }

        public async Task InsertRange(IEnumerable<T> enities)
        {
            await _db.AddRangeAsync(enities);
        }

        public void Update(T entity)
        {
            _db.Attach(entity);
            _context.Entry(entity).State = EntityState.Modified;

        }
    }
}
