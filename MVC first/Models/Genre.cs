

namespace MVC_first
{
    public class Genre
    {
            public int Id { get; set; }
            public string? Title { get; set; }

            public virtual ICollection<Movie>? Movies { get; set; }
    }
}
