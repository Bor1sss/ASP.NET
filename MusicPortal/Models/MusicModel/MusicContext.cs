using Microsoft.EntityFrameworkCore;

using MusicPortal.Models.User;

namespace MusicPortal.Models.MusicModel
{
    public class MusicContext : DbContext
    {
        public DbSet<Music> Musics { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MusicPortal.Models.User.User> Users { get; set; }

        public MusicContext(DbContextOptions<MusicContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {
               
            }
        }
    }
}
