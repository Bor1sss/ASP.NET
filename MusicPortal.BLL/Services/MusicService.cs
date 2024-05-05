using AutoMapper;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Entities.MusicModel;
using MusicPortal.DAL.IRepository;
using MusicPortal.BLL.Infrastucture;

namespace MusicPortal.BLL.Services
{
    public class MusicService : IMusicService
    {
        IUnityOfWork Database { get; set; }

        public MusicService(IUnityOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateMusic(MusicDTO playerDto)
        {
            var b=await Database.Genre.GetOne((int)playerDto.GenreID);
            var player = new Music
            {
                Id = playerDto.Id,
                Title = playerDto.Title,
                PosterPath = playerDto.PosterPath,
                MusicPath = playerDto.MusicPath,
                Genre=b,
                
            };
            await Database.Music.Create(player);
            await Database.Save();
        }

        public async Task UpdateMusic(MusicDTO playerDto)
        {
            var player = new Music
            {
                Id = playerDto.Id,
                Title = playerDto.Title,
                PosterPath = playerDto.PosterPath,
                MusicPath = playerDto.MusicPath,
           

            };
            Database.Music.Update(player);
            await Database.Save();
        }

        public async Task DeleteMusic(int id)
        {
            await Database.Music.Delete(id);
            await Database.Save();
        }

        public async Task<MusicDTO> GetMusic(int id)
        {
            var player = await Database.Music.GetOne(id);
            if (player == null)
                throw new ValidationException("Wrong player!", "");
            return new MusicDTO
            {
                Id = player.Id,
                Title = player.Title,
                PosterPath = player.PosterPath,
                MusicPath = player.MusicPath,
                GenreID = player.Genre.Id,
            };
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.

        public async Task<IEnumerable<MusicDTO>> GetMusics()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Music, MusicDTO>()
            .ForMember("Genre", opt => opt.MapFrom(c => c.Genre.Title)));
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Music>, IEnumerable<MusicDTO>>(await Database.Music.GetList());
        }

   

        public async Task Save()
        {
          await  Database.Music.Save();
        }

        public async Task<IQueryable<MusicDTO>> Incl()
        {
            var musics = (await Database.Music.Incl()).ToList();
            var musicDTOs = musics.Select(m => new MusicDTO
            {
                Id = m.Id,
                Title = m.Title,
                PosterPath = m.PosterPath,
                MusicPath = m.MusicPath,
                GenreID = m.Genre.Id,
                Genre = m.Genre.Title // Устанавливаем жанр для MusicDTO
            }).AsQueryable();

            return musicDTOs;
        }

        public async Task Update(MusicDTO musicModel)
        {
            var music = new Music
            {
                Id = musicModel.Id,
                Title = musicModel.Title,
                PosterPath = musicModel.PosterPath,
                MusicPath = musicModel.MusicPath,


            };
            Database.Music.Update(music);
        }
    }
}
