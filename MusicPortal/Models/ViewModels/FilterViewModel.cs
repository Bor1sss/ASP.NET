using Microsoft.AspNetCore.Mvc.Rendering;

namespace MusicPortal.Models.ViewModels
{
    public class FilterViewModel
    {

        public SelectList _Genres { get; } 
        public int SelectedGenre { get; }
        public string SelectedTitle{ get; } 


        public FilterViewModel(List<MusicModel.Genre> Geres, int genre, string title)
        {
        
            Geres.Insert(0, new MusicModel.Genre { Title = "All", Id = 0 });
            _Genres = new SelectList(Geres, "Id", "title", genre);
            SelectedGenre = genre;
            SelectedTitle = title;
        }
        





    }
}
