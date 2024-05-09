using System.Text.Json.Serialization;

namespace MusicPortal_WebApi.Models.MusicModel
{
    public class Genre
    {
        public int Id { get; set; }

        public string Title { get; set; }
        [JsonIgnore]
        public ICollection<Music>? Musics { get; set; }


    }
}
