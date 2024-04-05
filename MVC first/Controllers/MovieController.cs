using Microsoft.AspNetCore.Mvc;

namespace MVC_first.Controllers
{
    public class MovieController : Controller
    {
        Context db;
        public MovieController(Context context)
        {
            db = context;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Movie> movies = await Task.Run(() => db.Films);
            return View(movies);
        }
    }
}
