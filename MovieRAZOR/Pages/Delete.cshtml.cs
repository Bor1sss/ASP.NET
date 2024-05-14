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
    public class DeleteModel : PageModel
    {
        private readonly IRepository<Movie> _repository;

        public DeleteModel(IRepository<Movie> context)
        {
             _repository = context;
        }

        [BindProperty]
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

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await  _repository.GetById(id.Value);
            if (movie != null)
            {
                Movie = movie;
                await _repository.Delete(id.Value);
                await  _repository.Save();
            }

            return RedirectToPage("./Index");
        }
    }
}
