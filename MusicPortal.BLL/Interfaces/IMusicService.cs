
using MusicPortal.BLL.DTO;

namespace MusicPortal.BLL.Interfaces
{
    public interface IMusicService
    {
      
        Task CreateMusic(MusicDTO teamDto);
        Task UpdateMusic(MusicDTO teamDto);
        Task DeleteMusic(int id);
        Task<MusicDTO> GetMusic(int id);
        Task<IEnumerable<MusicDTO>> GetMusics();
        Task<IQueryable<MusicDTO>> Incl();
        Task Save();
        Task Update(MusicDTO musicModel);
    }
}

