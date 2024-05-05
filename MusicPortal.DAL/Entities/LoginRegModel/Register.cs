using System.ComponentModel.DataAnnotations;

namespace MusicPortal.DAL.Entities.LoginRegModel
{
    public class Register
    {
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        public string? Login { get; set; }
        [Required(ErrorMessage = "Поле не должно быть пустым!")]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessage = "Пароли не совпадают!")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }
    }
}
