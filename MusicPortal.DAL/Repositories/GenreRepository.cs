using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.IRepository;
using MusicPortal.DAL.Entities.MusicModel;
using MusicPortal.DAL.EF;

namespace MusicPortal.Models.IRepository
{
    public class GenreRepository : IRepository<Genre>
    {
  
        private readonly MusicContext _context;
        public GenreRepository(MusicContext context)
        {
            _context = context;
        }

        public async Task Create(Genre item)
        {
            await _context.Genres.AddAsync(item);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
           Genre? st = await _context.Genres.FindAsync(id);
            if (st != null)
            {
                _context.Genres.Remove(st);
                _context.SaveChanges();
            }
        }

        public async Task<List<Genre>> GetList()
        {
            var a = await _context.Genres.ToListAsync();
            return a;
        }

        public  async Task<Genre> GetOne(int id)
        {
            return await _context.Genres
           .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async void Update(Genre item)
        {
            _context.Update(item);
        }

        public async Task<Genre> GetByName(string name)
        {
            return await _context.Genres.FirstOrDefaultAsync(u => u.Title == name);
        }

        public async Task<IQueryable<Genre>> Incl()
        {
            return _context.Genres.Include(x => x.Title);
        }
    }
}
