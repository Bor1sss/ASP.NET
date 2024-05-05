using MusicPortal.DAL.Entities.MusicModel;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.BLL.DTO.LoginRegDTO
{
    // Data Transfer Object - специальная модель для передачи данных
    // Класс PlayerDTO должен содержать только те данные, которые нужно передать 
    // на уровень представления или, наоборот, получить с этого уровня.
    public class LoginDTO
    {
        [Required]
        public string? Login { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string? Password { get; set; }


    }
}
