using Microsoft.AspNetCore.Mvc;
using MusicPortal_WebApi.Models.User;
namespace MusicPortal_WebApi.IRepository.User
{

    public interface IRepositoryUser
    {
        Task<List<MusicPortal_WebApi.Models.User.User>> GetAllUsers();
        Task Create(MusicPortal_WebApi.Models.User.User item);
        Task<MusicPortal_WebApi.Models.User.User> GetUserByLoginAsync(string login);
        Task<MusicPortal_WebApi.Models.User.User> GetUser(int id);


        Task Delete(int id);
        Task Save();



    }



}
