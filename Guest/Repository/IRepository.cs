using Guest.Models;
using Microsoft.AspNetCore.Mvc;

namespace Guest.Repository
{
    public interface IRepository
    {

        Task<List<Messages>> GetMessageList();
        Task<List<Users>> GetAllUsers();
        Task<Messages> GetMessage(int id);
        Task Create(Messages item);
        Task Create(Users item);
        void Update(Messages item);
        Task Delete(int id);
        Task Save();
        Task<Users> GetUserByLoginAsync(string login);

    }
}
