using MusicPortal.Models.MusicModel;

namespace Models
{
    public class CombinedMessages
    {
        public MusicPortal.Models.MusicModel.Music? Music { get; set; }
        public IEnumerable<Music>? Musics { get; set; }
    }
}
