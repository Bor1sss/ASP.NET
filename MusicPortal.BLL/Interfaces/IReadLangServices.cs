using Microsoft.Extensions.Configuration;
using MusicPortal.DAL.Entities;

namespace MusicPortal.BLL.Interfaces
{
    public interface ILangRead
    {
        List<Language> languageList();
    }

}
