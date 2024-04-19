using MusicPortal.Models.MusicModel;
using MusicPortal.Models.ViewModels;

namespace Models
{
    public class CombinedMessages
    {
        public MusicPortal.Models.MusicModel.Music? Music { get; set; }
        //public IEnumerable<Music>? Musics { get; set; }

        public IndexViewModel Musics { get; set; }
    }
}
