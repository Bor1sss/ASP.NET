using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models;
using MusicPortal.Models.IRepository.Genre;
using MusicPortal.Models.IRepository.Music;
using MusicPortal.Models.MusicModel;
using MusicPortal.Models.ViewModels;
using MusicPortal.Models.ViewModels.Sort;
using Repository;

namespace MusicPortal.Controllers
{
    public class MusicModelsController : Controller
    {

        IMusicRep repo;
        IRepositoryUser repoU;
        IGenreRep genro;
        public MusicModelsController(IMusicRep r, IRepositoryUser u, IGenreRep g)
        {
            repo = r;
            repoU= u;
            genro = g;
        }


        [HttpPost]
        public IActionResult DownloadMusic(string musicPath)
        {

            var m = "Music/" + musicPath;
            var filePath = Path.Combine(repo._appEnvironment.WebRootPath, m);

            // Отправляем файл для скачивания
            return File(System.IO.File.OpenRead(filePath), "audio/mp4", Path.GetFileName(filePath));
        }
        // GET: MusicModels
        public async Task<IActionResult> Index(string title, int id = 0, int page = 1, SortState sortOrder = SortState.NameAsc)
        {

            var login = HttpContext.Session.GetString("Login");

            if (login != ""&&login!=null)
            {
                ViewBag.Name=login;
                var genres = genro.GetGenresList().Result; // Предполагается, что у тебя есть DbContext _dbContext и модель Genre

               
                ViewBag.Genres = genres;
                var b = await repo.GetMusicList();

                int pageSize = 5;
                IQueryable<Music> songs = await repo.Incl();

                if (id != 0)
                {
                    songs = songs.Where(p => p.Genre.Id == id);
                }
                if (!string.IsNullOrEmpty(title))
                {
                    songs = songs.Where(p => p.Title == title);
                }


                songs = sortOrder switch
                {
                    SortState.NameDesc => songs.OrderByDescending(s => s.Title),
                    SortState.AgeAsc => songs.OrderBy(s => s.Genre.Title),
                    SortState.AgeDesc => songs.OrderByDescending(s => s.Genre.Title),
                    _ => songs.OrderBy(s => s.Title),
   
                };
                // После фильтрации и Include, применяем пагинацию
                var count = await songs.CountAsync();

                var items = await songs.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
                IndexViewModel viewModel = new IndexViewModel(
                    items,
                    new PageViewModel(count, page, pageSize),
                    new FilterViewModel(await genro.GetGenresList(), id, title),
                    new SortViewModel(sortOrder)
                );

                return View(new CombinedMessages
                {
                    Musics = viewModel,
                });

                return View(viewModel);



                //return View(new CombinedMessages
                //{
                //    Musics = b
                //});
            }
            else
            {
                HttpContext.Session.SetString("Login", "");
                return RedirectToAction("Index", "Login");
            }
           
        }

        // GET: MusicModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var musicModel = await repo.GetMusic((int)id);
            if (musicModel == null)
            {
                return NotFound();
            }

            return View(musicModel);
        }

        // GET: MusicModels/Create
        public IActionResult Create()
        {
            var login = HttpContext.Session.GetString("Login");
            if (login != "Guest")
            {
                var verified = repoU.GetUserByLoginAsync(login).Result;
                if (verified.isVerified)
                {
                    var genres = genro.GetGenresList().Result;


                    ViewBag.Genres = genres;
                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "MusicModels");
                }
            }
            else
            {
                return RedirectToAction("Index", "MusicModels");
            }
        }

        // POST: MusicModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile music, IFormFile poster,[Bind("Id,Title")] Music musicModel,int genre)
        {
            var g = await genro.GetGenresList();
                        
            musicModel.Genre = await genro.GetGenre(genre);
            if (poster == null)
            {
                musicModel.PosterPath = " ";
            }
            else
            {
                musicModel.PosterPath = poster.FileName;
            }
           
           
            musicModel.MusicPath = music.FileName;
          
                if (music != null)
                {

                    string path = "/Images/" + poster.FileName;
                    string path2 = "/Music/" + music.FileName;


                    using (var fileStream = new FileStream(repo._appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await poster.CopyToAsync(fileStream);
                    }
                    using (var fileStream2 = new FileStream(repo._appEnvironment.WebRootPath + path2, FileMode.Create))
                    {
                        await music.CopyToAsync(fileStream2);
                    }
                    repo.Create(musicModel);
                    repo.Save();
                    return RedirectToAction(nameof(Index));
                }
      
            var genres = genro.GetGenresList().Result; // Предполагается, что у тебя есть DbContext _dbContext и модель Genre

            // Помещение списка жанров в ViewBag
            ViewBag.Genres = genres;
            return View(musicModel);
        }

        // GET: MusicModels/Edit/5


        public async Task<IActionResult> CreateGenre()
        {

            var login = HttpContext.Session.GetString("Login");
            if (login != "Guest")
            {
                var verified = repoU.GetUserByLoginAsync(login).Result;
                if (verified.isVerified)
                {

                    return View();
                }
                else
                {
                    return RedirectToAction("Index", "MusicModels");
                }
            }
            else
            {
                return RedirectToAction("Index", "MusicModels");
            }

        }
        public async Task<IActionResult> DeleteGenre(int id)
        {

            var login = HttpContext.Session.GetString("Login");
            if (login != "Guest" && login=="Admin")
            {
                await genro.Delete(id);
                await genro.Save();
                return RedirectToAction("Index", "MusicModels");

            }
            else
            {
                return RedirectToAction("Index", "MusicModels");
            }

        }
        
        public async Task<IActionResult> CreateGenre1([Bind("Id,Title")] Genre genre)
        {
            if (ModelState.IsValid && genro.GetGenreByName(genre.Title)!=null)
            {
                genro.Create(genre);
                genro.Save();
            }
            return RedirectToAction("Index", "MusicModels");


        }
        public async Task<IActionResult> Edit(int? id)
        {

            var genres = genro.GetGenresList().Result;


            ViewBag.Genres = genres;
            if (id == null)
            {
                return NotFound();
            }

            var M = await repo.GetMusic(id.Value);
            if (M == null)
            {
                return NotFound();
            }
            return View(M);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile music, IFormFile poster, int id, [Bind("Id,Title")] Music musicModel, int genre)
        {
            var g = genro.GetGenresList();
            musicModel.Genre = g.Result[genre - 1];
            musicModel.PosterPath = poster.FileName;
            musicModel.MusicPath = music.FileName;

            if (music != null)
            {

                string path = "/Images/" + poster.FileName;
                string path2 = "/Music/" + music.FileName;


                using (var fileStream = new FileStream(repo._appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await poster.CopyToAsync(fileStream);
                }
                using (var fileStream2 = new FileStream(repo._appEnvironment.WebRootPath + path2, FileMode.Create))
                {
                    await music.CopyToAsync(fileStream2);
                }
                repo.Update(musicModel);
                repo.Save();
                return RedirectToAction(nameof(Index));
            }

            var genres = genro.GetGenresList().Result; 

         
            ViewBag.Genres = genres;
            return View(musicModel);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            var genres = genro.GetGenresList().Result;
            if (id == null)
            {
                return NotFound();
            }

            var musicModel=await repo.GetMusic((int)id);
            if (musicModel == null)
            {
                return NotFound();
            }

            return View(musicModel);
        }

        // POST: MusicModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
          
           await repo.Delete(id);
           await repo.Save();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MusicModelExists(int id)
        {
            List<Music> list = await repo.GetMusicList();
            return (list?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
