using MusicPortal.DAL.Entities.MusicModel;
using System.ComponentModel.DataAnnotations;

namespace MusicPortal.BLL.DTO
{
    // Data Transfer Object - специальная модель для передачи данных
    // Класс PlayerDTO должен содержать только те данные, которые нужно передать 
    // на уровень представления или, наоборот, получить с этого уровня.
    public class MusicDTO
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Title")]
        public string? Title { get; set; }
        public string? PosterPath { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public string? MusicPath { get; set; }

        public int? GenreID { get; set;}
        public string? Genre { get; set; }

    }
}
