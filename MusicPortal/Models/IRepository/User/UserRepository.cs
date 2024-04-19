using Guest.Models;
using Microsoft.EntityFrameworkCore;
using MusicPortal.Models.MusicModel;
using MusicPortal.Models.User;

namespace Repository
{
    public class UserRepository : IRepositoryUser
    {

        private readonly MusicContext _context;
        public UserRepository(MusicContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByLoginAsync(string login)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == login);
        }
        public async Task Create(User item)
        {
            await _context.Users.AddAsync(item);
            _context.SaveChanges();
        }
        public async Task<List<User>> GetAllUsers()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.Users
             .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Delete(int id)
        {
            User? st = await _context.Users.FindAsync(id);
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
