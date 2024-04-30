using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
namespace Guest.Models
{
    public class Users
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string? Salt { get; set; }
        [JsonIgnore]
        public ICollection<Messages>? Messages { get; set; }

    }
}
