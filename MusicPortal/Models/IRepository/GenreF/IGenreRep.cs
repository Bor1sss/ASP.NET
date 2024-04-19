using MusicPortal.Models.MusicModel;
namespace MusicPortal.Models.IRepository.Genre
{
    public interface IGenreRep
    {


        Task<List<MusicModel.Genre>> GetGenresList();


        Task<MusicModel.Genre> GetGenre(int id);
        Task Create(MusicModel.Genre item);

        void Update(MusicModel.Genre item);

        Task<MusicModel.Genre> GetGenreByName(string name);

        Task Delete(int id);
        Task Save();

    }
}
