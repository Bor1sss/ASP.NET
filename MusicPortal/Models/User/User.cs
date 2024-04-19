using MusicPortal.Models.MusicModel;

namespace MusicPortal.Models.User
{
    public class User
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string? Salt { get; set; }

        public bool isVerified {  get; set; }=false;
        public ICollection<MusicModel.Music>? Musics { get; set; }

    }
}
