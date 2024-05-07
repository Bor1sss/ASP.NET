using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MusicPortal_WebApi.Models.MusicModel
{
    public class Music
    {
        public int Id { get; set; }

        public string? Title { get; set; }
        public string? PosterPath { get; set; }


        public string? MusicPath { get; set; }

        public virtual Genre? Genre { get; set; }



    }
}
