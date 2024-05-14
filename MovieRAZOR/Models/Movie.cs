using MVC_first.Annotations;
using System.ComponentModel.DataAnnotations;
namespace MVC_first
{
    public class Movie
    {

            public int Id { get; set; }

            [Required(ErrorMessage = "Поле должно быть установлено.")]
            [StringLength(50, MinimumLength = 3, ErrorMessage = "Длина строки должна быть от 3 до 50 символов")]
            [Display(Name = "Title")]
            public string? Title { get; set; }


            [Required(ErrorMessage = "Поле должно быть установлено.")]
            public string? Director { get; set; }


            [Required(ErrorMessage = "Поле должно быть установлено.")]
            [DataCheck(ErrorMessage ="Дата не может быть меньше 1900")]    
            public DateTime Date { get; set; }

            [Display(Name = "Poster Path")]
            public string? PosterPath { get; set; }
            [Required(ErrorMessage = "Поле должно быть установлено.")]
            public string? Description { get; set; }
             public string? Genre { get; set; }


    }
}
