using MusicPortal.BLL.DTO;
using MusicPortal.Models.ViewModels.Sort;

namespace MusicPortal.Models.ViewModels
{
    public class IndexViewModel
    {

        public IEnumerable<MusicDTO> Musics { get; set; }
        public PageViewModel PageViewModel { get; }
        public FilterViewModel FilterViewModel { get; }
        public SortViewModel SortViewModel { get; }

        public IndexViewModel(IEnumerable<MusicDTO> musics, PageViewModel pageViewModel,
            FilterViewModel filterViewModel, SortViewModel sortViewModel)
        {
            Musics = musics;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
        }


    }
}
