using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MusicPortal_WebApi.Models;

namespace MusicPortal_WebApi.IRepository.Music
{
    public class MusicRepository : IMusicRep
    {
        public IWebHostEnvironment _appEnvironment { get; set; }
        private readonly MusicContext _context;
        public MusicRepository(MusicContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }
        public async Task Create(Models.MusicModel.Music item)
        {
            await _context.Musics.AddAsync(item);
            _context.SaveChanges();
        }


        public async Task<Models.MusicModel.Music> DeleteMusic(int id)
        {
            Models.MusicModel.Music? st = await _context.Musics.FindAsync(id);
            if (st != null)
                _context.Musics.Remove(st);
            _context.SaveChanges();
            return st;
        }


        public async Task<List<Models.MusicModel.Music>> GetMusicList()
        {
            var a = await _context.Musics.ToListAsync();
            var b = await _context.Genres.ToListAsync();
            return a;
        }
        public async Task<IQueryable<Models.MusicModel.Music>> Incl()
        {
            return _context.Musics.Include(x => x.Genre);
        }



        [HttpGet("{id}")]
        public async Task<Models.MusicModel.Music> GetMusic(int id)
        {
            return await _context.Musics
             .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async void Update(Models.MusicModel.Music item)
        {

            _context.Update(item);
        }


    }

}
