using MusicPortal.DAL.Entities.MusicModel;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.BLL.DTO
{
    // Data Transfer Object - специальная модель для передачи данных
    // Класс PlayerDTO должен содержать только те данные, которые нужно передать 
    // на уровень представления или, наоборот, получить с этого уровня.
    public class RegisterDTO
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
