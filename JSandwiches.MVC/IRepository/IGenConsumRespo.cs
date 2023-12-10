namespace JSandwiches.MVC.IRepository
{
    public interface IGenConsumRepo<T> where T : class
    {
        Task<List<T>?> GetAll();
        Task<(T?, string?)> GetById(int id);
        Task<bool> Create(T entity);
        Task<(bool, T?)> Create2(T entity);
        Task<bool> Update(T entity, int id);
        Task<bool> Delete(int id);
    }
}
