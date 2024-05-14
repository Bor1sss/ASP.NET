

namespace MovieRAZOR.IRepository
{
    public interface IRepository<T> where T : class
    {
        Task<List<T>> GetList();
        Task<T> GetById(int id);
        Task<T> GetByName(string name);

        Task Create(T item);
        Task Update(T item);

        Task Delete(int id);
        Task Save();

        Task<bool> Exists(int id);

        Task Attach(T item);
    }
}
