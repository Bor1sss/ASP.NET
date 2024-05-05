using Microsoft.AspNetCore.Mvc.Rendering;
using MusicPortal.BLL.DTO;

namespace MusicPortal.Models.ViewModels
{
    public class FilterViewModel
    {

        public SelectList _Genres { get; } 
        public int SelectedGenre { get; }
        public string SelectedTitle{ get; } 


        public FilterViewModel(List<GenreDTO> Geres, int genre, string title)
        {
        
            Geres.Insert(0, new GenreDTO { Title = "All", Id = 0 });
            _Genres = new SelectList(Geres, "Id", "title", genre);
            SelectedGenre = genre;
            SelectedTitle = title;
        }
        





    }
}
