
using MusicPortal.BLL.DTO;
namespace UserPortal.BLL.Interfaces
{
    public interface IUserService
    {
        Task CreateUser(UserDTO teamDto);
        Task UpdateUser(UserDTO teamDto);
        Task DeleteUser(int id);
        Task<UserDTO> GetUser(int id);
        Task<IEnumerable<UserDTO>> GetUsers();
        Task Save();
        Task<UserDTO> GetUserByLogin(string name);
    }
}

