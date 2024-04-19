using Microsoft.AspNetCore.Mvc;
using MusicPortal.Models.User;

namespace Repository
{

    public interface IRepositoryUser
    {
        Task<List<User>> GetAllUsers();
        Task Create(User item);
        Task<User> GetUserByLoginAsync(string login);
        Task<User> GetUser(int id);


        Task Delete(int id);
        Task Save();



    }



}
