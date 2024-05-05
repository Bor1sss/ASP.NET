using System.ComponentModel.DataAnnotations;

namespace MusicPortal.DAL.Entities.LoginRegModel
{
    public class LoginModel
    {
        [Required]
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }
    }
}
