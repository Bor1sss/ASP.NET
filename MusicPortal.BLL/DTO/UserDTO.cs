using MusicPortal.DAL.Entities.MusicModel;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.BLL.DTO
{
    // Data Transfer Object - специальная модель для передачи данных
    // Класс PlayerDTO должен содержать только те данные, которые нужно передать 
    // на уровень представления или, наоборот, получить с этого уровня.
    public class UserDTO
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Password { get; set; }

        public string? Salt { get; set; }

        public bool isVerified { get; set; } = false;


    }
}
