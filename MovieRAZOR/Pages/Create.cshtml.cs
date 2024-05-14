using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieRAZOR.IRepository;
using MVC_first;

namespace MovieRAZOR.Pages
{
    public class CreateModel : PageModel
    {
        private readonly IRepository<Movie> _repository;
        IWebHostEnvironment _appEnvironment;
        public CreateModel(IRepository<Movie> context, IWebHostEnvironment appEnvironment)
        {
             _repository = context;
            _appEnvironment = appEnvironment;

        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        [BindProperty]
        public IFormFile UploadedFile { get; set; } = default!;

        public async Task<IActionResult> OnPostAsync()
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

             _repository.Create(Movie);
            await  _repository.Save();

            return RedirectToPage("./Index");
        }

    }
}
