using MusicPortal.DAL.Entities.MusicModel;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.BLL.DTO
{
    // Data Transfer Object - специальная модель для передачи данных
    // Класс PlayerDTO должен содержать только те данные, которые нужно передать 
    // на уровень представления или, наоборот, получить с этого уровня.
    public class RegisterDTO
    {
        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                 ErrorMessageResourceName = "NameReq")]
        [Display(Name = "Title", ResourceType = typeof(Resources.Resource))]
        public string? Login { get; set; }


        [Required(ErrorMessageResourceType = typeof(Resources.Resource),
                ErrorMessageResourceName = "PasswordReq")]
        [Display(Name = "Password", ResourceType = typeof(Resources.Resource))]
        [DataType(DataType.Password)]
        public string? Password { get; set; }

        [Compare("Password", ErrorMessageResourceType = typeof(Resources.Resource), ErrorMessageResourceName = "PasswordMatch")]
        [DataType(DataType.Password)]
        public string? ConfirmPassword { get; set; }


    }
}
