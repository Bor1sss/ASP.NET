using Microsoft.EntityFrameworkCore;
using MusicPortal_WebApi.Models;

namespace MusicPortal_WebApi.IRepository.GenreF
{
    public class GenreRepository : IGenreRep
    {

        private readonly MusicContext _context;
        public GenreRepository(MusicContext context)
        {
            _context = context;
        }

        public async Task Create(Models.MusicModel.Genre item)
        {
            await _context.Genres.AddAsync(item);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            Models.MusicModel.Genre? st = await _context.Genres.FindAsync(id);
            if (st != null)
            {
                _context.Genres.Remove(st);
                _context.SaveChanges();
            }
        }

        public async Task<List<Models.MusicModel.Genre>> GetGenresList()
        {
            var a = await _context.Genres.ToListAsync();
            return a;
        }

        public async Task<Models.MusicModel.Genre> GetGenre(int id)
        {
            return await _context.Genres
           .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async void Update(Models.MusicModel.Genre item)
        {
            _context.Update(item);
        }

        public async Task<Models.MusicModel.Genre> GetGenreByName(string name)
        {
            return await _context.Genres.FirstOrDefaultAsync(u => u.Title == name);
        }
    }
}
