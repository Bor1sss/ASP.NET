
using MusicPortal.BLL.DTO;
using MusicPortal.Models.ViewModels;

namespace Models
{
    public class CombinedMessages
    {
        public MusicDTO? Music { get; set; }
        //public IEnumerable<Music>? Musics { get; set; }

        public IndexViewModel Musics { get; set; }
    }
}
