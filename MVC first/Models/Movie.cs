namespace MVC_first
{
    public class Movie
    {

            public int Id { get; set; }
            public string? Title { get; set; }
            public string? Director { get; set; }
            public DateTime Date { get; set; }
            public string? PosterPath { get; set; }
            public string? Description { get; set; }
            public virtual ICollection<Genre>? Genres { get; set; }
  

    }
}
