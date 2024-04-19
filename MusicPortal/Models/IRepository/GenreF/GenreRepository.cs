using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages;
using MusicPortal.Models.MusicModel;

namespace MusicPortal.Models.IRepository.Genre
{
    public class GenreRepository:IGenreRep
    {

        private readonly MusicContext _context;
        public GenreRepository(MusicContext context)
        {
            _context = context;
        }

        public async Task Create(MusicModel.Genre item)
        {
            await _context.Genres.AddAsync(item);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            MusicModel.Genre? st = await _context.Genres.FindAsync(id);
            if (st != null)
            {
                _context.Genres.Remove(st);
                _context.SaveChanges();
            }
        }

        public async Task<List<MusicModel.Genre>> GetGenresList()
        {
            var a = await _context.Genres.ToListAsync();
            return a;
        }

        public  async Task<MusicModel.Genre> GetGenre(int id)
        {
            return await _context.Genres
           .FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async void Update(MusicModel.Genre item)
        {
            _context.Update(item);
        }

        public async Task<MusicModel.Genre> GetGenreByName(string name)
        {
            return await _context.Genres.FirstOrDefaultAsync(u => u.Title == name);
        }
    }
}
