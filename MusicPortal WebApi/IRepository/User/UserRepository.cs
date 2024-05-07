
using Microsoft.EntityFrameworkCore;
using MusicPortal_WebApi.Models;
using MusicPortal_WebApi.Models.User;
namespace MusicPortal_WebApi.IRepository.User
{
    public class UserRepository : IRepositoryUser
    {

        private readonly MusicContext _context;
        public UserRepository(MusicContext context)
        {
            _context = context;
        }

        public async Task<MusicPortal_WebApi.Models.User.User> GetUserByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == login);
        }
        public async Task Create(MusicPortal_WebApi.Models.User.User item)
        {
            await _context.Users.AddAsync(item);
            _context.SaveChanges();
        }
        public async Task<List<MusicPortal_WebApi.Models.User.User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<MusicPortal_WebApi.Models.User.User> GetUser(int id)
        {
            return await _context.Users
             .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Delete(int id)
        {
            MusicPortal_WebApi.Models.User.User? st = await _context.Users.FindAsync(id);
            if (st != null)
                _context.Users.Remove(st);
            _context.SaveChanges();
        }

        public async Task Save()
        {
            _context.SaveChanges();
        }
    }



}
