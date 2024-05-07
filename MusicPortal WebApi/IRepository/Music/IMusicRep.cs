using MusicPortal_WebApi.Models.MusicModel;
namespace MusicPortal_WebApi.IRepository.Music
{
    public interface IMusicRep
    {
        public IWebHostEnvironment _appEnvironment { get; }

        Task<List<MusicPortal_WebApi.Models.MusicModel.Music>> GetMusicList();


        Task<Models.MusicModel.Music> GetMusic(int id);
        Task Create(Models.MusicModel.Music item);

        void Update(Models.MusicModel.Music item);


        Task<IQueryable<Models.MusicModel.Music>> Incl();
        Task<Models.MusicModel.Music> DeleteMusic(int id);
        Task Save();



    }
}
