using System.ComponentModel.DataAnnotations;
namespace Guest.Models
{
    public class Users
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string? Salt { get; set; }
        public ICollection<Messages>? Messages { get; set; }

    }
}
