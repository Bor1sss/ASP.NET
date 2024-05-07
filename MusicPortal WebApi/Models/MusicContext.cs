using Microsoft.EntityFrameworkCore;
using MusicPortal_WebApi.Models.MusicModel;
using MusicPortal_WebApi.Models.User;
namespace MusicPortal_WebApi.Models
{
    public class MusicContext : DbContext
    {
        public DbSet<Music> Musics { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<MusicPortal_WebApi.Models.User.User> Users { get; set; }

        public MusicContext(DbContextOptions<MusicContext> options)
           : base(options)
        {
            if (Database.EnsureCreated())
            {

            }
        }
    }
}
