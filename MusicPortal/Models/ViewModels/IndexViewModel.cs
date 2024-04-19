namespace MusicPortal.Models.ViewModels
{
    public class IndexViewModel
    {

        public IEnumerable<MusicModel.Music> Musics { get; set; }
        public PageViewModel PageViewModel { get; }
        public FilterViewModel FilterViewModel { get; }
        //public SortViewModel SortViewModel { get; }

        public IndexViewModel(IEnumerable<MusicModel.Music> musics, PageViewModel pageViewModel,
            FilterViewModel filterViewModel /*SortViewModel sortViewModel*/)
        {
            Musics = musics;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            //SortViewModel = sortViewModel;
        }


    }
}
