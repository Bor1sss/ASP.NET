using AutoMapper;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Entities.MusicModel;
using MusicPortal.DAL.IRepository;
using MusicPortal.BLL.Infrastucture;
using GenrePortal.BLL.Interfaces;

namespace GenrePortal.BLL.Services
{
    public class GenreService : IGenreService
    {
        IUnityOfWork Database { get; set; }

        public GenreService(IUnityOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateGenre(GenreDTO playerDto)
        {
            var player = new Genre
            {
                Id = playerDto.Id,
                Title = playerDto.Title,
              
                
            };
            await Database.Genre.Create(player);
            await Database.Save();
        }

        public async Task UpdateGenre(GenreDTO playerDto)
        {
            var player = new Genre
            {
                Id = playerDto.Id,
                Title = playerDto.Title,
              

            };
            Database.Genre.Update(player);
            await Database.Save();
        }

        public async Task DeleteGenre(int id)
        {
            await Database.Genre.Delete(id);
            await Database.Save();
        }

        public async Task<GenreDTO> GetGenre(int id)
        {
            var player = await Database.Genre.GetOne(id);
            if (player == null)
                throw new ValidationException("Wrong player!", "");
            return new GenreDTO
            {
                Id = player.Id,
                Title = player.Title,
           
            };
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.

        public async Task<IEnumerable<GenreDTO>> GetGenres()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<Genre, GenreDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<Genre>, IEnumerable<GenreDTO>>(await Database.Genre.GetList());
        }

        public async Task<GenreDTO> GetGenreByName(string name)
        {
            var b= await Database.Genre.GetByName(name);
            var genre = new GenreDTO
            {
                Id = b.Id,
                Title = b.Title,
                

            };



            return genre;
        }
        public async Task Save()
        {
            await Database.Genre.Save();
        }
    }
}
