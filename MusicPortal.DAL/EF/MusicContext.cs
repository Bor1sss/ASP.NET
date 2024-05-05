using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.Entities.MusicModel;
using MusicPortal.DAL.Entities.User;

namespace MusicPortal.DAL.EF
{
    public class MusicContext : DbContext
    {
        public DbSet<Music> Musics { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<User> Users { get; set; }

        public MusicContext(DbContextOptions<MusicContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {

            }
        }
    }
}
