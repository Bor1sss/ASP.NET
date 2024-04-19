using System.ComponentModel.DataAnnotations;

namespace MusicPortal.Models.MusicModel
{
    public class Music
    {
        public int Id { get; set; }


        [Required(ErrorMessage = "Поле должно быть установлено.")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
        [Display(Name = "Title")]
        public string? Title { get; set; }
        public string? PosterPath { get; set; }

        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public string? MusicPath { get; set; }
        [Required(ErrorMessage = "Поле должно быть установлено.")]
        public virtual Genre? Genre { get; set; }



    }
}
