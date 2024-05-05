using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.IRepository;
using MusicPortal.DAL.Entities.MusicModel;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Entities.User;

namespace MusicPortal.Models.IRepository
{
    public class UserRepository : IRepository<User>
    {

        private readonly MusicContext _context;
        public UserRepository(MusicContext context)
        {
            _context = context;
        }

        public async Task Create(User item)
        {
            await _context.Users.AddAsync(item);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            User? st = await _context.Users.FindAsync(id);
            if (st != null)
            {
                _context.Users.Remove(st);
                _context.SaveChanges();
            }
        }

        public async Task<List<User>> GetList()
        {
            var a = await _context.Users.ToListAsync();
            return a;
        }

        public async Task<User> GetOne(int id)
        {
            return await _context.Users
           .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async void Update(User item)
        {
            _context.Update(item);
        }

        public async Task<User> GetByName(string name)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task<IQueryable<User>> Incl()
        {
            return _context.Users.Include(x => x.Name);
        }
    }
}
