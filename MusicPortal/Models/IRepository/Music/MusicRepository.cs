using Microsoft.EntityFrameworkCore;
using MusicPortal.Models.MusicModel;

namespace MusicPortal.Models.IRepository.Music
{
    public class MusicRepository : IMusicRep
    {
        public IWebHostEnvironment _appEnvironment {  get; set; }
        private readonly MusicContext _context;
        public MusicRepository(MusicContext context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }
        public async Task Create(MusicModel.Music item)
        {
            await _context.Musics.AddAsync(item);
            _context.SaveChanges();
        }

        public async Task Delete(int id)
        {
            MusicModel.Music? st = await _context.Musics.FindAsync(id);
            if (st != null)
                _context.Musics.Remove(st);
            _context.SaveChanges();
        }

 

        public async Task<List<MusicModel.Music>> GetMusicList()
        {
            var a = await _context.Musics.ToListAsync();
            var b = await _context.Genres.ToListAsync();
            return a;
        }
        public async Task<IQueryable<MusicModel.Music>> Incl()
        {
            return  _context.Musics.Include(x => x.Genre);
        }


        public async Task<MusicModel.Music> GetMusic(int id)
        {
            return await _context.Musics
             .FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public async void Update(MusicModel.Music item)
        {
            _context.Update(item);
        }


    }
  
    }
