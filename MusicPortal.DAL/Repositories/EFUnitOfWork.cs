using MusicPortal.DAL.EF;
using MusicPortal.DAL.Entities.MusicModel;
using MusicPortal.DAL.Entities.User;
using MusicPortal.DAL.IRepository;
using MusicPortal.Models.IRepository;
namespace MusicPortal.DAL.Repositories
{
    /*
     * Паттерн Unit of Work позволяет упростить работу с различными репозиториями и дает уверенность, 
     * что все репозитории будут использовать один и тот же контекст данных.
    */

    public class EFUnitOfWork : IUnityOfWork
    {
        private MusicContext db;
        private UserRepository userRepository;
        private MusicRepository musicRepository;
        private GenreRepository genreRepository;

        public EFUnitOfWork(MusicContext context)
        {
            db = context;
        }

        public IRepository<Genre> Genre
        {
            get
            {
                if (genreRepository == null)
                    genreRepository = new GenreRepository(db);
                return genreRepository;
            }
        }

        public IRepository<Music> Music
        {
            get
            {
                if (musicRepository == null)
                    musicRepository = new MusicRepository(db);
                return musicRepository;
            }
        }

        public IRepository<User> User
        {
            get
            {
                if (userRepository == null)
                    userRepository = new UserRepository(db);
                return userRepository;
            }
        }

        public async Task Save()
        {
            await db.SaveChangesAsync();
        }

    }
}