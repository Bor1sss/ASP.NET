using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MovieRAZOR.IRepository;
using MVC_first;

namespace MovieRAZOR.Pages
{
    public class DetailsModel : PageModel
    {
        private readonly IRepository<Movie> _repository;

        public DetailsModel(IRepository<Movie> context)
        {
            _repository = context;
        }

        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await  _repository.GetById(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            else
            {
                Movie = movie;
            }
            return Page();
        }
    }
}
