using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Guest.Models
{
    public class Messages
    {

        public int Id { get; set; }

        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        public string Message { get; set; }
        public DateTime MessageDate { get; set; }
     
        public Users? User { get; set; }
    }


}
