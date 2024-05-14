using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MVC_first;

namespace MovieRAZOR.Pages
{
    public class CreateModel : PageModel
    {
        private readonly MVC_first.Context _context;
        IWebHostEnvironment _appEnvironment;
        public CreateModel(MVC_first.Context context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
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

            _context.Films.Add(Movie);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }

    }
}
