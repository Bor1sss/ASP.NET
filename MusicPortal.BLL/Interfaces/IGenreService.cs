using MusicPortal.BLL.DTO;

namespace GenrePortal.BLL.Interfaces
{
    public interface IGenreService
    {
        Task CreateGenre(GenreDTO teamDto);
        Task UpdateGenre(GenreDTO teamDto);
        Task DeleteGenre(int id);
        Task<GenreDTO> GetGenre(int id);
        Task<IEnumerable<GenreDTO>> GetGenres();
        Task<GenreDTO> GetGenreByName(string name);
        Task Save();

    }
}

