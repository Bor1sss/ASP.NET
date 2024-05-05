using MusicPortal.DAL.Entities.MusicModel;
using MusicPortal.DAL.Entities.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.IRepository
{
    public interface IUnityOfWork
    {

        IRepository<Genre> Genre { get; }
        IRepository<Music> Music { get; }

        IRepository<User> User { get; }
        Task Save();


    }
}
