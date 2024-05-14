using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MovieRAZOR.IRepository;
using MVC_first;

namespace MovieRAZOR.Pages
{
    public class EditModel : PageModel
    {
        private readonly IRepository<Movie> _repository;

        IWebHostEnvironment _appEnvironment;
        public EditModel(IRepository<Movie> context,IWebHostEnvironment appEnvironment)
        {
             _repository = context;
            _appEnvironment = appEnvironment;

        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _repository.GetById(id.Value);
            if (movie == null)
            {
                return NotFound();
            }
            Movie = movie;
            return Page();
        }
        [BindProperty]
        public IFormFile UploadedFile { get; set; } = default!;

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

           await _repository.Attach(Movie);

            try
            {
                if (!ModelState.IsValid)
                {
                    return Page();
                }
                
                if (UploadedFile != null && UploadedFile.Length > 0)
                {
                    var uploadsFolder = Path.Combine(_appEnvironment.WebRootPath, "Images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + UploadedFile.FileName;
                    var filePath = Path.Combine(uploadsFolder, uniqueFileName);

                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await UploadedFile.CopyToAsync(fileStream);
                    }

                    Movie.PosterPath = "/Images/" + uniqueFileName; // сохраняем путь к файлу в объекте Movie
                }
                await  _repository.Save();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MovieExists(Movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool MovieExists(int id)
        {
            return  _repository.Exists(id).Result;
        }
    }
}
