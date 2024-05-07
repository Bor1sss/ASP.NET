using MusicPortal_WebApi.Models.MusicModel;

namespace MusicPortal_WebApi.Models.User
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
