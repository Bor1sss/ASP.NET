using MusicPortal.Models.MusicModel;
namespace MusicPortal.Models.IRepository.Music
{
    public interface IMusicRep
    {
        public IWebHostEnvironment _appEnvironment {  get; }

        Task<List<MusicModel.Music>> GetMusicList();

  
        Task<MusicModel.Music> GetMusic(int id);
        Task Create(MusicModel.Music item);

        void Update(MusicModel.Music item);



        Task Delete(int id);
        Task Save();



    }
}
