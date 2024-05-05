using MusicPortal.DAL.Entities.MusicModel;

namespace MusicPortal.DAL.Entities.User
{
    public class User
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string? Salt { get; set; }

        public bool isVerified { get; set; } = false;
        public ICollection<Music>? Musics { get; set; }

    }
}
