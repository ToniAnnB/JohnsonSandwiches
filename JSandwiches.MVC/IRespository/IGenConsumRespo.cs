namespace JSandwiches.MVC.IRespository
{
    public interface IGenConsumRespo<T> where T : class
    {
        Task<List<T>?> GetAll();
        Task<(T?, string?)> GetById(int id);
        Task<bool> Create(T entity);
        Task<bool> Update(T entity, int id);
        Task<bool> Delete(int id);
    }
}
