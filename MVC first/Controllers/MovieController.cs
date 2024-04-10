using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MVC_first.Controllers
{
    public class MovieController : Controller
    {
        Context db;

        IWebHostEnvironment _appEnvironment;
        public MovieController(Context context, IWebHostEnvironment appEnvironment)
        {
            db = context;
            _appEnvironment = appEnvironment;
        }

        public async Task<IActionResult> View(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await db.Films
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Movie = await db.Films.FindAsync(id);
            if (Movie == null)
            {
                return NotFound();
            }
            return View(Movie);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(IFormFile uploadedFile,int id, [Bind("Id,Title,Director,Date,PosterPath,Description")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }
            if (!uploadedFile.FileName.Contains(".jpg")|| !uploadedFile.FileName.Contains(".png"))
            {
                ModelState.AddModelError("", "файл должен быть .jpg или png");
            }
            if (ModelState.IsValid)
            {
                try
                {
                    if (uploadedFile != null)
                    {
                       
                        string path = "/Images/" + uploadedFile.FileName;


                        using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                        {
                            await uploadedFile.CopyToAsync(fileStream);
                        }
                        movie.PosterPath = path;
                        db.Update(movie);
                        await db.SaveChangesAsync();
                        return RedirectToAction(nameof(Index));
                    }
                  
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> movies = await Task.Run(() => db.Films);
            return View(movies);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(IFormFile uploadedFile,[Bind("Id,Title,Director,Date,PosterPath,Description")] Movie movie)
        {
            if (!uploadedFile.FileName.Contains(".jpg") || !uploadedFile.FileName.Contains(".png"))
            {
                ModelState.AddModelError("", "файл должен быть .jpg или png");
            }
            if (ModelState.IsValid)
            {
                if (uploadedFile != null)
                {
                    
                    string path = "/Images/" + uploadedFile.FileName; 

                    
                    using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
                    {
                        await uploadedFile.CopyToAsync(fileStream); 
                    }
                    movie.PosterPath = path;
                    db.Add(movie);
                    await db.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
               
            }
            return View(movie);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await db.Films
                .FirstOrDefaultAsync(m => m.Id == id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var Movie = await db.Films.FindAsync(id);
            if (Movie != null)
            {
                db.Films.Remove(Movie);
            }

            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return db.Films.Any(e => e.Id == id);
        }







        [HttpPost]
        [RequestSizeLimit(1000000000)]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile)
        {
           

            return RedirectToAction("Index");

        }







    }



}
