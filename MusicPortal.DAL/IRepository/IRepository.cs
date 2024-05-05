namespace MusicPortal.DAL.IRepository
{
    public interface IRepository<T> where T : class
    {


        Task<List<T>> GetList();


        Task<T> GetOne(int id);
        Task Create(T item);

        void Update(T item);

        Task<T> GetByName(string name);

        Task Delete(int id);
        Task Save();

        Task<IQueryable<T>> Incl();
    }
}
