using AutoMapper;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.DAL.Entities.MusicModel;
using MusicPortal.DAL.IRepository;
using MusicPortal.BLL.Infrastucture;
using UserPortal.BLL.Interfaces;
using MusicPortal.DAL.Entities.User;
using System.Numerics;

namespace MusicPortal.BLL.Services
{
    public class UserService : IUserService
    {
        IUnityOfWork Database { get; set; }

        public UserService(IUnityOfWork uow)
        {
            Database = uow;
        }

        public async Task CreateUser(UserDTO playerDto)
        {
            var player = new User
            {
                Id = playerDto.Id,
                Name = playerDto.Name,
                Password = playerDto.Password,
                isVerified=playerDto.isVerified,
                Salt=playerDto.Salt,    
              
                
            };
            await Database.User.Create(player);
            await Database.Save();
        }

        public async Task UpdateUser(UserDTO playerDto)
        {
            var player = new User
            {
                Id = playerDto.Id,
                Name = playerDto.Name,
                Password = playerDto.Password,
                isVerified = playerDto.isVerified,
                Salt = playerDto.Salt,


            };
            Database.User.Update(player);
            await Database.Save();
        }

        public async Task DeleteUser(int id)
        {
            await Database.User.Delete(id);
            await Database.Save();
        }

        public async Task<UserDTO> GetUser(int id)
        {
            var player = await Database.User.GetOne(id);
            if (player == null)
                throw new ValidationException("Wrong player!", "");
            return new UserDTO
            {
                Id = player.Id,
                Name = player.Name,
                Password = player.Password,
                isVerified = player.isVerified,
                Salt = player.Salt,

            };
        }

        // Automapper позволяет проецировать одну модель на другую, что позволяет сократить объемы кода и упростить программу.

        public async Task<IEnumerable<UserDTO>> GetUsers()
        {
            var mapper = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>()).CreateMapper();
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(await Database.User.GetList());
        }

        public  async Task Save()
        {
           await Database.Save();
        }

        public async Task<UserDTO> GetUserByLogin(string name)
        {
            var b = await Database.User.GetByName(name);
            var user = new UserDTO
            {
                Id = b.Id,
                Name = b.Name,
                Password = b.Password,
                isVerified = b.isVerified,
                Salt = b.Salt,



            };


            return user;
        }
        
    }
}
