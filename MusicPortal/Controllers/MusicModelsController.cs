using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Linq;
using System.Threading.Tasks;
using GenrePortal.BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Elfie.Diagnostics;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Models;
using MusicPortal.BLL.DTO;
using MusicPortal.BLL.Interfaces;
using MusicPortal.Models.ViewModels;
using MusicPortal.Models.ViewModels.Sort;
using MusicPortal.ViewModels;
using UserPortal.BLL.Interfaces;


namespace MusicPortal.Controllers
{
    [Culture]
    public class MusicModelsController : Controller
    {
        public IWebHostEnvironment _appEnvironment { get; }

        IMusicService repo;
        IUserService repoU;
        IGenreService genro;
        public MusicModelsController(IMusicService r, IUserService u, IGenreService g, IWebHostEnvironment appEnvironment)
        {
            repo = r;
            repoU = u;
            genro = g;
            _appEnvironment = appEnvironment;
        }


        [HttpPost]
        public IActionResult DownloadMusic(string musicPath)
        {

            var m = "Music/" + musicPath;
            var filePath = Path.Combine(_appEnvironment.WebRootPath, m);

            // Отправляем файл для скачивания
            return File(System.IO.File.OpenRead(filePath), "audio/mp4", Path.GetFileName(filePath));
        }
        // GET: MusicModels
        public async Task<IActionResult> Index(string title, int id = 0, int page = 1, SortState sortOrder = SortState.NameAsc)
        {
            HttpContext.Session.SetString("path", Request.Path);
            var login = HttpContext.Session.GetString("Login");

            if (login != ""&&login!=null)
            {
                ViewBag.Name=login;
                var genres = genro.GetGenres().Result; // Предполагается, что у тебя есть DbContext _dbContext и модель Genre

               
                ViewBag.Genres = genres;
                var b = await repo.GetMusics();

                int pageSize = 5;
                IQueryable<MusicDTO> songs = await repo.Incl();

                if (id != 0)
                {
                    songs = songs.Where(p => p.GenreID == id);
                }
                if (!string.IsNullOrEmpty(title))
                {
                    songs = songs.Where(p => p.Title == title);
                }


                songs = sortOrder switch
                {
                    SortState.NameDesc => songs.OrderByDescending(s => s.Title),
                    SortState.AgeAsc => songs.OrderBy(s => s.Genre),
                   SortState.AgeDesc => songs.OrderByDescending(s => s.Genre),
                    _ => songs.OrderBy(s => s.Title),
   
                };
                // После фильтрации и Include, применяем пагинацию
                var count = songs.Count();

                var items =  songs.Skip((page - 1) * pageSize).Take(pageSize).ToList();
                IndexViewModel viewModel = new IndexViewModel(
                    items,
                    new PageViewModel(count, page, pageSize),
                    new FilterViewModel((await genro.GetGenres()).ToList(), id, title),
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
                var verified = repoU.GetUserByLogin(login).Result;
                if (verified.isVerified)
                {
                    var genres = genro.GetGenres().Result;


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
        public async Task<IActionResult> Create(IFormFile music, IFormFile poster,[Bind("Id,Title")] MusicDTO musicModel,int genre)
        {
            var g = await genro.GetGenres();
                        
            musicModel.GenreID = (await genro.GetGenre(genre)).Id;
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


                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await poster.CopyToAsync(fileStream);
                    }
                    using (var fileStream2 = new FileStream(_appEnvironment.WebRootPath + path2, FileMode.Create))
                    {
                        await music.CopyToAsync(fileStream2);
                    }
                    await repo.CreateMusic(musicModel);
                     await  repo.Save();
                    return RedirectToAction(nameof(Index));
                }
      
            var genres = genro.GetGenres().Result; // Предполагается, что у тебя есть DbContext _dbContext и модель Genre

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
                var verified = repoU.GetUserByLogin(login).Result;
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
                await genro.DeleteGenre(id);
                await genro.Save();
                return RedirectToAction("Index", "MusicModels");

            }
            else
            {
                return RedirectToAction("Index", "MusicModels");
            }

        }
        
        public async Task<IActionResult> CreateGenre1([Bind("Id,Title")] GenreDTO genre)
        {
            if (ModelState.IsValid && genro.GetGenreByName(genre.Title)!=null)
            {
                await genro.CreateGenre(genre);
                genro.Save();
            }
            return RedirectToAction("Index", "MusicModels");


        }
        public async Task<IActionResult> Edit(int? id)
        {

            var genres = genro.GetGenres().Result;


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
        public async Task<IActionResult> Edit(IFormFile music, IFormFile poster, int id, [Bind("Id,Title")] MusicDTO musicModel, int genre)
        {
            var g = genro.GetGenres();
            //musicModel.GenreID = g.Result[genre - 1];
            musicModel.PosterPath = poster.FileName;
            musicModel.MusicPath = music.FileName;

            if (music != null)
            {

                string path = "/Images/" + poster.FileName;
                string path2 = "/Music/" + music.FileName;


                using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                {
                    await poster.CopyToAsync(fileStream);
                }
                using (var fileStream2 = new FileStream(_appEnvironment.WebRootPath + path2, FileMode.Create))
                {
                    await music.CopyToAsync(fileStream2);
                }
                repo.Update(musicModel);
                repo.Save();
                return RedirectToAction(nameof(Index));
            }

            var genres = genro.GetGenres().Result; 

         
            ViewBag.Genres = genres;
            return View(musicModel);
        }


        public async Task<IActionResult> Delete(int? id)
        {
            var genres = genro.GetGenres().Result;
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
          
           await repo.DeleteMusic(id);
           repo.Save();
            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> MusicModelExists(int id)
        {
            List<MusicDTO> list = (await repo.GetMusics()).ToList();
            return (list?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
