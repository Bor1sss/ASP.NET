using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.IRepository;
using MusicPortal.DAL.Entities.MusicModel;
using MusicPortal.DAL.EF;

namespace MusicPortal.Models.IRepository
{
    public class MusicRepository : IRepository<Music>
    {

        private readonly MusicContext _context;
        public MusicRepository(MusicContext context)
        {
            _context = context;
        }

        public async Task Create(Music item)
        {
            await _context.Musics.AddAsync(item);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            Music? st = await _context.Musics.FindAsync(id);
            if (st != null)
            {
                _context.Musics.Remove(st);
                _context.SaveChanges();
            }
        }

        public async Task<List<Music>> GetList()
        {
            var a = await _context.Musics.ToListAsync();
            return a;
        }

        public async Task<Music> GetOne(int id)
        {
            return await _context.Musics
           .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async void Update(Music item)
        {
            _context.Update(item);
        }

        public async Task<Music> GetByName(string name)
        {
            return await _context.Musics.FirstOrDefaultAsync(u => u.Title == name);
        }

        public async Task<IQueryable<Music>> Incl()
        {
            return _context.Musics.Include(x => x.Genre);
        }
    }
}
