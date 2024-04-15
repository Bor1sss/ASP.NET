using Guest.Models;
using Microsoft.AspNetCore.Mvc;

namespace Guest.Repository
{
    public interface IRepositoryMessage
    {

        Task<List<Messages>> GetMessageList();
     
        Task<Messages> GetMessage(int id);
        Task Create(Messages item);
     
        void Update(Messages item);
        Task Delete(int id);
        Task Save();
    

    }
    public interface IRepositoryUser
    {
        Task<List<Users>> GetAllUsers();
        Task Create(Users item);
        Task<Users> GetUserByLoginAsync(string login);
    }



}
