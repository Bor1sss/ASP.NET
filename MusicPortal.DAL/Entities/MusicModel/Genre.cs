namespace MusicPortal.DAL.Entities.MusicModel
{
    public class Genre
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public ICollection<Music>? Musics { get; set; }


    }
}
