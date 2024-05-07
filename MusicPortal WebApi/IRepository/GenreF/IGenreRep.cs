using MusicPortal_WebApi.Models.MusicModel;
namespace MusicPortal_WebApi.IRepository.GenreF
{
    public interface IGenreRep
    {


        Task<List<Genre>> GetGenresList();


        Task<Genre> GetGenre(int id);
        Task Create(Genre item);

        void Update(Genre item);

        Task<Genre> GetGenreByName(string name);

        Task Delete(int id);
        Task Save();

    }
}
